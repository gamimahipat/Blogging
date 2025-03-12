using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace BloggingAPI.Generic
{
    public static class PasswordHelper
    {
        private static readonly PasswordHasher<object> _passwordHasher = new PasswordHasher<object>();

        public static string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, enteredPassword) == PasswordVerificationResult.Success;
        }
        public static string GenerateSalt(int size = 16)
        {
            byte[] salt = new byte[size];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = Encoding.UTF8.GetBytes(password + salt);
                var hash = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
