using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Admin.Command
{
    public class UserRegisterCommand : IRequest<Guid>
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = string.Empty;
        public string MobileNo { get; set; } = null!;

        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public string Password { get; set; } = null!;
        public string Role { get; set; } = "User";
        public string CreatedBy { get; set; } = "Sushil Rai(Admin)";
    }

    public class UserRegisterCommandHandler(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider,
        IPasswordHasher hasher)
        : IRequestHandler<UserRegisterCommand, Guid>
    {
        public async Task<Guid> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower()
                                          || u.Username.ToLower() == request.Username.ToLower(), cancellationToken);

            if (existingUser is not null)
            {
                throw new Exception("User with the same email or username already exists.");
            }
            
            //Password Hashing
            hasher.CreatePasswordHash(request.Password, out string passwordHash, out string passwordSalt);

            var user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                Username = string.IsNullOrWhiteSpace(request.Username) ? request.Email : request.Username,
                MobileNo = request.MobileNo,
                Address = request.Address,
                IsActive = request.IsActive,
                Role = request.Role,

                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,

                CreatedBy = string.IsNullOrWhiteSpace(userServiceProvider.UserName) ? "Self-Register" : userServiceProvider.UserName,
                CreatedOn = DateTime.UtcNow
            };
            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
