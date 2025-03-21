using FluentMigrator;

namespace BloggingAPI.Database
{
    [Migration(1001)]
    public class V1001_Create_Roles : Migration
    {
        public override void Up()
        {
            Create.Table("Roles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Description").AsString(300).Nullable()
                .AddDefaultColumns();

            // Seed default roles
            Insert.IntoTable("Roles").Row(new { Name = "SuperAdmin", Description = "Has full access" });
            Insert.IntoTable("Roles").Row(new { Name = "Admin", Description = "Manages users and content" });
            Insert.IntoTable("Roles").Row(new { Name = "User", Description = "Basic user role" });
            Insert.IntoTable("Roles").Row(new { Name = "ReportUser", Description = "Can access reports" });
            Insert.IntoTable("Roles").Row(new { Name = "AccountUser", Description = "Manages accounts" });
        }

        public override void Down()
        {
            Delete.Table("Roles");
        }
    }
}
