using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BloggingAPI.v1
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IConfiguration _config;

        public JwtHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(int userId, string userName, string email, string mobile, List<string> roleNames, List<string> permissions)
        {
            try
            {
                IConfigurationSection jwtConfig = _config.GetSection("JwtConfig");

                string? secretKey = jwtConfig["SecretKey"];
                string? issuer = jwtConfig["Issuer"];
                string? audience = jwtConfig["Audience"];
                int ExpirationInMinutes = int.Parse(jwtConfig["ExpirationInMinutes"]?.ToString() ?? "180");

                if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
                {
                    throw new Exception("JWT configuration values are missing in appsettings.json.");
                }

                SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(secretKey));
                SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.MobilePhone, mobile),
                };

                foreach (string roleName in roleNames)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                }

                foreach (string permission in permissions)
                {
                    claims.Add(new Claim("Permission", permission));
                }

                //claims.AddRange(roleNames.Select(roleId => new Claim("roleNames", roleId.ToString())));

                JwtSecurityToken token = new(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(ExpirationInMinutes),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error generating JWT token");
                throw;
            }
        }
    }
}
