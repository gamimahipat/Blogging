using BloggingAPI.Generic;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BloggingAPI.v1
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Roles>> GetAllRolesAsync()
        {
            try
            {
                return await _context.Roles.ToListAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching all roles.");
                return new List<Roles>();
            }
        }

        public async Task<Roles?> GetRoleByIdAsync(int id)
        {
            try
            {
                return await _context.Roles.FindAsync(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error fetching role with ID {RoleId}.", id);
                return null;
            }
        }

        public async Task<bool> RoleExistsAsync(int id)
        {
            try
            {
                return await _context.Roles.AnyAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error checking if role with ID {RoleId} exists.", id);
                return false;
            }
        }

        public async Task AddRoleAsync(Roles role)
        {
            try
            {
                await _context.Roles.AddAsync(role);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error adding new role: {@Role}.", role);
                throw;
            }
        }

        public async Task UpdateRoleAsync(Roles role)
        {
            try
            {
                _context.Entry(role).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error updating role with ID {RoleId}.", role.Id);
                throw;
            }
        }

        public async Task DeleteRoleAsync(Roles role)
        {
            try
            {
                _context.Roles.Remove(role);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error deleting role with ID {RoleId}.", role.Id);
                throw;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error saving changes to the database.");
                throw;
            }
        }
    }
}
