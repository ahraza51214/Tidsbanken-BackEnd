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
        Version = "v1",
        Title = "Tidsbanken API",
        Description = "Tidsbanken API serves as a platform for storing and managing vacation request related data. " +
        "Utilizing ASP.NET Core with Entity Framework Core and SQL Server, " +
        "the API encapsulates functionalities for user, ineligible period, comment and vacation request. It allows users to: \n\n" +

            "- **Create and Maintain Users**: " +
                "Store basic information such as name, username, password, and E-mail for each user. \n\n" +

            "- **Catalog Movies**: Organize movies with essential details including title, genre, release year, " +
                "director, poster image, and trailer link. \n\n" +

            "- **Organize Franchises**: Manage franchises, each potentially encompassing multiple movies, " +
                "with an associated description. \n\n" +

        "Key API functionalities: \n\n" +

            "- **CRUD Operations**: Full Create, Read, Update, and Delete functionalities for movies, " +
                "characters, and franchises. \n\n" +

            "- **Relational Updates**: Specifically tailored endpoints for updating character associations " +
                "in movies and movie associations in franchises. \n\n" +

            "- **Reporting**: Generate reports to fetch movies in a franchise, characters in a movie, " +
                "and characters within a particular franchise. \n\n" +

        "This API uses DTOs to ensure a decoupled client experience and to safeguard against over - posting. " +
        "It also ensures documentation clarity through Swagger / OpenAPI annotations. \n\n" +

        "**Note**: Ensure to adhere to the documentation when interacting with the endpoints and follow the business rules to maintain the integrity of the data relationships."
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