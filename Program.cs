using Invoce_Hub.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Invoce_Hub.Services.Implementations;
using Invoce_Hub.Services;
using Invoce_Hub.Repositories.Implementations;
using Invoce_Hub.Repositories;
using System.Reflection;

Env.Load(); 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger/OpenAPI services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Invoice Hub API",
        Version = "v1",
        Description = "API para gestión de facturas e invoices",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Invoice Hub",
            Email = "support@invoicehub.com"
        }
    });

    // Include XML comments if available
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var connectionString = builder.Configuration.GetConnectionString("InvoiceHubDatabase") 
    ?? Environment.GetEnvironmentVariable("INVOICE_HUB_DB_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Connection string 'InvoiceHubDatabase' not found.");

builder.Services.AddDbContext<InvoiceHubDbContext>(options =>
    options.UseNpgsql(connectionString)
);


//Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();


var app = builder.Build();

// Ejecuta las migraciones en el arranque, usando el proveedor de servicios del host
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<InvoiceHubDbContext>();
        db.Database.Migrate(); 
        logger.LogInformation("Database migrated successfully.");

    }
    catch(Exception ex)
    {
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Invoice Hub API v1");
        c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root URL
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();