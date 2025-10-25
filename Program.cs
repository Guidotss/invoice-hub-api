using Invoce_Hub.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;
using Invoce_Hub.Services.Implementations;
using Invoce_Hub.Services;
using Invoce_Hub.Repositories.Implementations;
using Invoce_Hub.Repositories;

Env.Load(); 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var connectionString = builder.Configuration.GetConnectionString("InvoiceHubDatabase") 
    ?? Environment.GetEnvironmentVariable("INVOICE_HUB_DB_CONNECTION_STRING")
    ?? throw new InvalidOperationException("Connection string 'InvoiceHubDatabase' not found.");

builder.Services.AddOpenApi();
builder.Services.AddDbContext<InvoiceHubDbContext>(options =>
    options.UseNpgsql(connectionString)
);


//Services
builder.Services.AddScoped<IUserService, UserService>();

//Repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();



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
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}