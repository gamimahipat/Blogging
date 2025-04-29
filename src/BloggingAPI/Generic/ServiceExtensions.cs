using Serilog;

namespace BloggingAPI.Generic
{
    public static class ServiceExtensions
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            LoggingConfiguration.ConfigureSerilog(builder);
            builder.Host.UseSerilog();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.ConfigureSwagger();
            services.ConfigureDatabase(configuration);
            services.AddRepositories();
            services.ConfigureAuthentication(configuration);
            services.ConfigureFluentMigrator(configuration);
            services.AddAuthorization(options =>
            {
                AuthorizationPolicies.AddCustomAuthorizationPolicies(options);
            });
            services.AddEndpointsApiExplorer();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngularDev", policy =>
                {
                    policy.WithOrigins("http://localhost:51345") // Frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });
        }

        public static void ConfigureMiddlewares(this WebApplication app)
        {
            app.Services.ApplyMigrations();
            app.UseCors("AllowAngularDev");
            app.UseSwaggerWithUI();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty; // if you want swagger at root path
            });
        }
    }
}
