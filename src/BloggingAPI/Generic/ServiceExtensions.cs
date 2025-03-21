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
        }

        public static void ConfigureMiddlewares(this WebApplication app)
        {
            app.Services.ApplyMigrations();
            app.UseSwaggerWithUI();
            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}
