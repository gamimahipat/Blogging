using FluentMigrator.Builders.Create.Table;
using FluentMigrator;
using FluentMigrator.Runner;
using System.Reflection;

namespace BloggingAPI
{
    public static class FluentMigratorExtensions
    {
        public static void ConfigureFluentMigrator(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Database connection string is missing.");
            }

            services.AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(FluentMigratorExtensions).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }

        public static void ApplyMigrations(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var migrator = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            migrator.MigrateUp();
        }

        
    }

    public static class MigrationExtensions
    {
        public static ICreateTableColumnOptionOrWithColumnSyntax AddDefaultColumns(
            this ICreateTableColumnOptionOrWithColumnSyntax table)
        {
            return table
                .WithColumn("CreatedOn").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("CreatedBy").AsInt32().Nullable()
                .WithColumn("UpdatedOn").AsDateTime().Nullable()
                .WithColumn("UpdatedBy").AsInt32().Nullable()
                .WithColumn("VersionNo").AsInt32().NotNullable().WithDefaultValue(1)
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true);
        }
    }
}
