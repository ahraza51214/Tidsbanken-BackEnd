using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Tidsbanken_BackEnd.Data;
using Tidsbanken_BackEnd.Services;
using Tidsbanken_BackEnd.Services.CommentService;
using Tidsbanken_BackEnd.Services.IneligiblePeriodService;
using Tidsbanken_BackEnd.Services.UserService;
using Tidsbanken_BackEnd.Services.VacationRequestService;

var builder = WebApplication.CreateBuilder(args);

// Adding services to the container.
builder.Services.AddControllers();

// Registering Services and ServiceFacade
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IIneligiblePeriodService, IneligiblePeriodService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IVacationRequestService, VacationRequestService>();
builder.Services.AddScoped<ServiceFacade>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Adding CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});

// Adding Swagger Documentation
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Tidsbanken Vacation Management API",
        Version = "v1",
        Description = "Welcome to the Tidsbanken Vacation Management API!\n\n" +
                       "This API provides a comprehensive solution to address challenges faced by employees when managing vacation requests and coordinating periods of absence.\n" +
                       "In the past, employees encountered difficulties in effective communication and planning, leading to scheduling conflicts and resource allocation problems during vacation periods.\n\n" +
                       "Key Features and Benefits:\n" +
                       "- Streamlined Vacation Request Workflow: Submit, review, and approve vacation requests with ease.\n" +
                       "- Shared Vacation Calendar: View colleagues' approved vacation days for better coordination.\n" +
                       "- Enhanced Communication: Bridge communication gaps and improve interactions amongst employees and managers for vacation requests.\n" +
                       "- Empower Managers: Managers can make informed decisions regarding resource allocation.\n\n" +
                       "Our goal is to establish a cohesive and productive work environment that benefits both employees and the organization. \n" +
                       "This API plays a vital role in achieving this goal by simplifying vacation management, optimizing team coordination, and enhancing overall efficiency.\n\n" +
                       "For more details and usage guidelines, please refer to the API documentation on https://github.com/ahraza51214/Tidsbanken-BackEnd.",
        TermsOfService = new Uri("https://example.com/terms"),
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
    
// Configuring Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(opt => opt
              .TokenValidationParameters = new TokenValidationParameters()
              {
                  ValidateIssuer = true,    
                  ValidIssuer = "https://keycloak-middleware.azurewebsites.net/realms/noroff",
                  ValidateAudience = false, // jwt doesnt include this claim
                  IssuerSigningKeyResolver = (token, securityToken, kid, parameters) =>
                  {
                      var client = new HttpClient();
                      var keyuri = "https://keycloak-middleware.azurewebsites.net/realms/noroff/protocol/openid-connect/certs";
                      //Retrieves the keys from keycloak instance to verify token
                      var response = client.GetAsync(keyuri).Result;
                      var responseString = response.Content.ReadAsStringAsync().Result;
                      var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(responseString);
                      return keys.Keys;
                  },    
              });

// Adding EF
builder.Services.AddDbContext<TidsbankenDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Ali")));

// Adding automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    }); 
}

// Use CORS policy in the middleware pipeline
app.UseCors("AllowMyOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();