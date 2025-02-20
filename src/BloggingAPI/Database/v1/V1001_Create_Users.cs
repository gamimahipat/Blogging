using FluentMigrator;

namespace BloggingAPI.Database
{
    [Migration(1001)]
    public partial class V1001_Create_Users : Migration
    {
        public override void Up()
        {
            Create.Table("Users")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Department").AsString(50).Nullable();
        }

        public override void Down()
        {
            Delete.Table("Users");
        }

        
    }
}
