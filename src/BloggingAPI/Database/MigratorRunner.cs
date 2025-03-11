using FluentMigrator.Builders.Create.Table;
using FluentMigrator;
using FluentMigrator.Runner;
using System.Reflection;

namespace BloggingAPI.Database
{
    public static class MigratorRunner
    {
        public static void MigrateDB(string conString, string schemaName, Assembly assembly)
        {
            IServiceProvider serviceProvider = CreateServices(conString, schemaName, assembly);

            // Put the database update into a scope to ensure
            // that all resources will be disposed.
            using IServiceScope scope = serviceProvider.CreateScope();
            UpdateDatabase(scope.ServiceProvider);
        }

        /// <summary>
        /// Configure the dependency injection services
        /// </summary>
        private static IServiceProvider CreateServices(string conString, string schemaName, Assembly assembly)
        {
            return new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddSqlServer()  // Change for other DBs like AddMySql, AddPostgres, etc.
                    .WithGlobalConnectionString(conString)
                    .ScanIn(assembly).For.Migrations()  // <-- Fixed this line
                )
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .BuildServiceProvider(false);  // Don't forget to build the provider
        }

        /// <summary>
        /// Update the database
        /// </summary>
        private static void UpdateDatabase(IServiceProvider serviceProvider)
        {
            // Instantiate the runner
            IMigrationRunner runner = serviceProvider.GetRequiredService<IMigrationRunner>();

            // Execute the migrations
            runner.MigrateUp();
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
