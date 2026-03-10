using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Admin.Command
{
    public class LoginUserCommand : IRequest<LoginResponseModel>
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class LoginUserCommandHandler(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IPasswordHasher hasher)
        : IRequestHandler<LoginUserCommand, LoginResponseModel>
    {
        public async Task<LoginResponseModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var user = await context.Users
                .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

            if (user == null || !hasher.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new LoginResponseModel()
                {
                   IsSuccess = false,
                   Message = "Invalid email or password"
                };
                //throw new Exception("Invalid email or password");
            }

            return new LoginResponseModel()
            {
                IsSuccess = true,
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                MobileNo = user.MobileNo,
                Username = user.Username,
                Role = user.Role
            };
        }
    }
}
