using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Helpers
{
    public interface IPasswordHasher
    {
        void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
        bool VerifyPasswordHash(string password, string storedHash, string storedSalt);
        string GeneratePassword(
            int length = 8,
            bool useUppercase = true,
            bool useLowercase = true,
            bool useDigits = true,
            bool useSymbols = true);
    }
}
