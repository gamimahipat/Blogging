using BloggingAPI.Generic;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.ConfigureLogging();

// Add Services
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure Middleware
app.ConfigureMiddlewares();

Log.Information("Application is starting...");
app.Run();
