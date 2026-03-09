using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Helpers
{
    public interface IPasswordHasher
    {
        void CreatePasswordHash(string password, out string passwordHash, out string passwordSalt);
        bool VerifyPasswordHash(string password, string storedHash, string storedSalt);
    }
}
