using FluentMigrator;

namespace BloggingAPI.Database
{
    [Migration(1002)]
    public class V1002_Create_Users : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(50).NotNullable()
                .WithColumn("MobileNo").AsString(15).NotNullable()
                .WithColumn("Password").AsString(100).NotNullable()
                .AddDefaultColumns();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
