using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();