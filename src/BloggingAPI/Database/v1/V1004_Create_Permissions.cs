using FluentMigrator;

namespace BloggingAPI.Database.v1
{
    [Migration(1004)]
    public class V1004_Create_Permissions : Migration
    {
        public override void Up()
        {
            Create.Table("Permissions")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable().Unique()
                .WithColumn("Description").AsString(300).Nullable()
                .AddDefaultColumns();
        }

        public override void Down()
        {
            Delete.Table("Permissions");
        }
    }
}
