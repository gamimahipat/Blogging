namespace BloggingAPI.v1
{
    public interface IRolesRepository
    {
        Task<IEnumerable<Roles>> GetAllRolesAsync();
        Task<Roles?> GetRoleByIdAsync(int id);
        Task<bool> RoleExistsAsync(int id);
        Task AddRoleAsync(Roles role);
        Task UpdateRoleAsync(Roles role);
        Task DeleteRoleAsync(Roles role);
        Task SaveChangesAsync();
    }
}
