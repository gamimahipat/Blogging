using FluentMigrator;
using System.Data;

namespace BloggingAPI.Database.v1
{
    [Migration(1005)]
    public class V1005_Create_RolePermissions : Migration
    {
        public override void Up()
        {
            Create.Table("RolePermissions")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("RoleId").AsInt32().NotNullable()
                .WithColumn("PermissionId").AsInt32().NotNullable()
                .AddDefaultColumns();

            // Foreign Keys
            Create.ForeignKey("FK_RolePermissions_Roles")
                .FromTable("RolePermissions").ForeignColumn("RoleId")
                .ToTable("Roles").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);

            Create.ForeignKey("FK_RolePermissions_Permissions")
                .FromTable("RolePermissions").ForeignColumn("PermissionId")
                .ToTable("Permissions").PrimaryColumn("Id")
                .OnDelete(Rule.Cascade);
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_RolePermissions_Roles").OnTable("RolePermissions");
            Delete.ForeignKey("FK_RolePermissions_Permissions").OnTable("RolePermissions");
            Delete.Table("RolePermissions");
        }
    }
}
