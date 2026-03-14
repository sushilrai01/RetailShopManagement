using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace RetailShopManagement.Infrastructure.Helpers
{
    public class PasswordHasher(ApplicationDbContext context) : IPasswordHasher
    {
        private static readonly Random random = new Random();

        // Character sets
        private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";
        private const string Symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        public void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = Convert.ToBase64String(hmac.Key);

                var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                passwordHash = Convert.ToBase64String(hashBytes);
            }
        }
        public bool VerifyPasswordHash(string password, string storedHash, string storedSalt)
        {
            var saltBytes = Convert.FromBase64String(storedSalt);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                var computedHashString = Convert.ToBase64String(computedHash);

                return computedHashString == storedHash;
            }
        }
        public string GeneratePassword(
            int length = 8,
            bool useUppercase = true,
            bool useLowercase = true,
            bool useDigits = true,
            bool useSymbols = true)
        {
            // Build character pool based on options
            StringBuilder charPool = new StringBuilder();

            if (useUppercase) charPool.Append(Uppercase);
            if (useLowercase) charPool.Append(Lowercase);
            if (useDigits) charPool.Append(Digits);
            if (useSymbols) charPool.Append(Symbols);

            // Ensure at least one character type is selected
            if (charPool.Length == 0)
            {
                throw new ArgumentException("At least one character type must be enabled");
            }

            // Generate password
            StringBuilder password = new StringBuilder(length);
            string pool = charPool.ToString();

            for (int i = 0; i < length; i++)
            {
                password.Append(pool[random.Next(pool.Length)]);
            }

            return password.ToString();
        }
    }
}
