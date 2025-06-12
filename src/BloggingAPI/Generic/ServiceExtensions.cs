using Serilog;

namespace BloggingAPI.Generic
{
    public static class ServiceExtensions
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            LoggingConfiguration.ConfigureSerilog(builder);
            _ = builder.Host.UseSerilog();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            _ = services.AddControllers();
            services.ConfigureSwagger();
            services.ConfigureDatabase(configuration);
            services.AddRepositories();
            services.ConfigureAuthentication(configuration);
            services.ConfigureFluentMigrator(configuration);
            _ = services.AddAuthorization(options =>
            {
                AuthorizationPolicies.AddCustomAuthorizationPolicies(options);
            });
            _ = services.AddEndpointsApiExplorer();

            _ = services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    _ = policy.WithOrigins("http://localhost:51345") // Frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public static void ConfigureMiddlewares(this WebApplication app)
        {
            app.Services.ApplyMigrations();
            _ = app.UseCors("AllowAngularDev");
            app.UseSwaggerWithUI();
            _ = app.UseSerilogRequestLogging();
            _ = app.UseHttpsRedirection();
            _ = app.UseAuthentication();
            _ = app.UseAuthorization();
            _ = app.MapControllers();
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; // if you want swagger at root path
            });
        }
    }
}
