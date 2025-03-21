using BloggingAPI.v1;

namespace BloggingAPI.Generic
{
    public static class RepositoryServiceExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();

        }
    }
}
