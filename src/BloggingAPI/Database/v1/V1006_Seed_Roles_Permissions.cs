using FluentMigrator;

namespace BloggingAPI.Database.v1
{
    [Migration(1006)]
    public class V1006_Seed_Roles_Permissions : Migration
    {
        public override void Up()
        {
            // Insert default permissions
            Insert.IntoTable("Permissions").Row(new { Name = "ManageUsers", Description = "Can add/edit/delete users" });
            Insert.IntoTable("Permissions").Row(new { Name = "ManagePosts", Description = "Can add/edit/delete posts" });
            Insert.IntoTable("Permissions").Row(new { Name = "ViewReports", Description = "Can view reports" });
            Insert.IntoTable("Permissions").Row(new { Name = "ManageAccounts", Description = "Can manage accounts" });

            // Assign permissions to roles
            Execute.Sql(@"
                INSERT INTO RolePermissions (RoleId, PermissionId)
                SELECT r.Id, p.Id
                FROM Roles r, Permissions p
                WHERE (r.Name = 'SuperAdmin' OR r.Name = 'Admin') AND 
                      (p.Name = 'ManageUsers' OR p.Name = 'ManagePosts');

                INSERT INTO RolePermissions (RoleId, PermissionId)
                SELECT r.Id, p.Id
                FROM Roles r, Permissions p
                WHERE r.Name = 'ReportUser' AND p.Name = 'ViewReports';

                INSERT INTO RolePermissions (RoleId, PermissionId)
                SELECT r.Id, p.Id
                FROM Roles r, Permissions p
                WHERE r.Name = 'AccountUser' AND p.Name = 'ManageAccounts';
            ");

        }
        public override void Down()
        {
            Delete.FromTable("RolePermissions").AllRows();
            Delete.FromTable("Permissions").AllRows();
        }
    }
}
