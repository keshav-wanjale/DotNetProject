using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiExample.WebApp.Services;  // Added for IUserService/UserService

var builder = WebApplication.CreateBuilder(args);

// Configure configuration sources
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Optional: retrieve configuration values
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var yourKeyValue = builder.Configuration["AppSettings:YourKey"];

// Register services
builder.Services.AddSingleton(connectionString);
builder.Services.AddSingleton(yourKeyValue);
builder.Services.AddControllers();

// Register application services
builder.Services.AddScoped<IUserService, UserService>();  // Register UserService for DI

var app = builder.Build();

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();