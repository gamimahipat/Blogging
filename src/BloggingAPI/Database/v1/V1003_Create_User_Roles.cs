using FluentMigrator;
using System.Data;

namespace BloggingAPI.Database.v1
{
    [Migration(1003)]
    public class V1003_Create_User_Roles : Migration
    {
        public override void Up()
        {
            Create.Table("UserRoles")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("UserId").AsInt32().NotNullable()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .AddDefaultColumns();

            // Add foreign key to Users table
            Create.ForeignKey("FK_UserRoles_Users")
                .FromTable("UserRoles").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            // Add foreign key to Roles table
            Create.ForeignKey("FK_UserRoles_Roles")
                .FromTable("UserRoles").ForeignColumn("RoleId")
                .ToTable("Roles").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_UserRoles_Users").OnTable("UserRoles");
            Delete.ForeignKey("FK_UserRoles_Roles").OnTable("UserRoles");

            Delete.Table("UserRoles");
        }
    }
}
