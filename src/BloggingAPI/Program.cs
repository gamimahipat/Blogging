using BloggingAPI.Generic;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Configure Serilog
builder.ConfigureLogging();

// Add Services
builder.Services.ConfigureServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure Middleware
app.ConfigureMiddlewares();

Log.Information("Application is starting...");
app.Run();
