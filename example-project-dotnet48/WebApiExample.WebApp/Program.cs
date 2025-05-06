using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using WebApiExample.WebApp;
using WebApiExample.WebApp.Services;
using WebApiExample.Common.DataAccess;
using WebApiExample.DataStore;

var builder = WebApplication.CreateBuilder(args);

// Load configuration sources
builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Configure services via Startup class
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

// Register application services with appropriate lifetimes
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UserContext>();

var app = builder.Build();

// Configure middleware & endpoints via Startup
startup.Configure(app, app.Environment);

app.Run();