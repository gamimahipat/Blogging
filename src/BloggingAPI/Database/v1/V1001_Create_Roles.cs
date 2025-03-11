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
        }

        public override void Down()
        {
            Delete.Table("Roles");
        }
    }
}
