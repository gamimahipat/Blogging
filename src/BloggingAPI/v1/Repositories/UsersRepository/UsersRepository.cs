using BloggingAPI.Generic;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BloggingAPI.v1
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IJwtHelper _jwtHelper;
        public UsersRepository(ApplicationDbContext context, IConfiguration configuration, IJwtHelper jwtHelper)
        {
            _context = context;
            _configuration = configuration;
            _jwtHelper = jwtHelper;
        }

        public async Task<ApiResponse> GetUsers()
        {
            try
            {
                FormattableString query = $@"SELECT * FROM Users WITH (NOLOCK)";

                List<Users> users = await _context.Users.FromSqlInterpolated(query).AsNoTracking().ToListAsync();

                return new ApiResponse(true, "User retrieved successfully.", users);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetUsers : Error while fetching users list.");
                return new ApiResponse(false, "Internal server error");
            }
        }

        public async Task<ApiResponse> GetUserById(int id)
        {
            try
            {
                FormattableString query = $@"SELECT * FROM Users WITH (NOLOCK) WHERE Id = {id}";

                Users? user = await _context.Users.FromSqlInterpolated(query).AsNoTracking().FirstOrDefaultAsync();

                return user == null ? new ApiResponse(false, "User not found", null)
                    : new ApiResponse(true, "User retrieved successfully.", user);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GetUserById : Error while fetching users.");
                return new ApiResponse(false, "Internal server error");
            }
        }

        public async Task<ApiResponse> PostUsers(UserSaveViewModel user)
        {
            try
            {
                var existingUser = await _context.Users.Where(u => u.UserName == user.UserName || u.Email == user.Email || u.MobileNo == user.MobileNo)
                                    .Select(u => new { u.UserName, u.Email, u.MobileNo }).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    List<string> conflicts = new();
                    if (existingUser.UserName == user.UserName) conflicts.Add("Username");
                    if (existingUser.Email == user.Email) conflicts.Add("Email");
                    if (existingUser.MobileNo == user.MobileNo) conflicts.Add("Mobile");

                    return new ApiResponse(false, $"{string.Join(", ", conflicts)} already exist.", null);
                }

                using Microsoft.EntityFrameworkCore.Storage.IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync();

                // Generate Salt and Hashed Password
                string salt = PasswordHelper.GenerateSalt();
                string hashedPassword = PasswordHelper.HashPassword(user.Password, salt);

                Users users = new()
                {
                    UserName = user.UserName,
                    Name = user.Name,
                    Email = user.Email,
                    MobileNo = user.MobileNo,
                    PasswordHash = hashedPassword,
                    Salt = salt,
                    ProfileImageUrl = user.ProfileImageUrl,
                    Bio = user.Bio
                };

                _ = await _context.Users.AddAsync(users);
                _ = await _context.SaveChangesAsync();

                UserRoles userRole = new()
                {
                    UserId = users.Id,
                    RoleId = user.RoleId
                };
                _ = await _context.UserRoles.AddAsync(userRole);
                _ = await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return new ApiResponse(true, "User created successfully.", null);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "PostUsers : Error while adding user");
                return new ApiResponse(false, "Internal server error", null);
            }
        }

        public async Task<ApiResponse> AuthenticateUser(LoginViewModel login)
        {
            try
            {
                Users? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == login.UserName || u.Email == login.UserName || u.MobileNo == login.UserName);

                if (user == null)
                    return new ApiResponse(false, "Incorrect username.", null);

                string hashedPassword = PasswordHelper.HashPassword(login.Password, user.Salt);

                if (user.PasswordHash != hashedPassword)
                    return new ApiResponse(false, "Incorrect password.", null);

                FormattableString query = $@"SELECT r.Name FROM UserRoles ur WITH (NOLOCK)
                                  JOIN Roles r WITH(NOLOCK) ON  ur.RoleId = r.Id
                                  WHERE ur.UserId = {user.Id}";

                List<string> roleNames = await _context.Roles.FromSqlInterpolated(query).Select(r => r.Name).AsNoTracking().ToListAsync();

                query = $@"SELECT DISTINCT p.Name FROM UserRoles ur WITH (NOLOCK)
                            JOIN Roles r WITH (NOLOCK) ON ur.RoleId = r.Id
                            JOIN RolePermissions rp WITH (NOLOCK) ON rp.RoleId = r.Id
                            JOIN Permissions p WITH (NOLOCK) ON rp.PermissionId = p.Id
                            WHERE ur.UserId = {user.Id}";

                List<string> permissions = await _context.Permissions.FromSqlInterpolated(query).Select(p => p.Name).AsNoTracking().ToListAsync();


                if (roleNames == null || roleNames.Count == 0)
                    roleNames = new List<string> { "User" };

                string token = _jwtHelper.GenerateJwtToken(user.Id, user.UserName, user.Email, user.MobileNo, roleNames, permissions);

                var responseData = new
                {
                    UserName = user.UserName,
                    Token = token,
                    Roles = roleNames,
                    Permissions = permissions
                };

                return new ApiResponse(true, "Login successfully.", responseData);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "AuthenticateUser: Error during user login.");
                return new ApiResponse(false, "Internal server error", null);
            }
        }

        public async Task UpdateUserAsync(Users user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                _ = await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Check Serilog Message: Error while updating user {UserId}", user.Id);
                throw;
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                Users? user = await _context.Users.FindAsync(id);
                if (user != null)
                {
                    _ = _context.Users.Remove(user);
                    _ = await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Check Serilog Message: Error while deleting user with ID {UserId}", id);
                throw;
            }
        }

        public async Task<bool> UserExistsAsync(int id)
        {
            try
            {
                return await _context.Users.AnyAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Check Serilog Message: Error checking existence of user with ID {UserId}", id);
                return false;
            }
        }

    }
}
