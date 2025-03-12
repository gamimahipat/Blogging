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
                .WithColumn("UserName").AsString(100).NotNullable().Unique()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(50).NotNullable().Unique()
                .WithColumn("MobileNo").AsString(15).NotNullable().Unique()
                .WithColumn("PasswordHash").AsString(100).NotNullable()
                .WithColumn("Salt").AsString(50).NotNullable()
                .WithColumn("ProfileImageUrl").AsCustom("NVARCHAR(MAX)").Nullable()
                .WithColumn("Bio").AsString(500).Nullable()
                .AddDefaultColumns();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }
    }
}
