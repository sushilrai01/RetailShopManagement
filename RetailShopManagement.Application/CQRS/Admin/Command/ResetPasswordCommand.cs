using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Admin.Command
{
    public class ResetPasswordCommand : IRequest<BaseResponseModel>
    {
        public Guid Id { get; set; }
    }

    public class ResetPasswordCommandHandler(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider,
        IPasswordHasher hasher)
        : IRequestHandler<ResetPasswordCommand, BaseResponseModel>
    {
        public async Task<BaseResponseModel> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingUser =
                await context.Users.FindAsync([request.Id], cancellationToken);

            if (existingUser is null)
            {
                throw new Exception($"User not found with Id: {request.Id}");
            }

            // Generate random password
            var newPassword = hasher.GeneratePassword(length: 8);

            //Password Hashing
            hasher.CreatePasswordHash(newPassword, out string passwordHash, out string passwordSalt);

            existingUser.PasswordHash = passwordHash;
            existingUser.PasswordSalt = passwordSalt;

            existingUser.LastModifiedBy = userServiceProvider.UserName;
            existingUser.LastModifiedOn = DateTime.Now;

            await context.SaveChangesAsync(cancellationToken);

            return new BaseResponseModel()
            {
                IsSuccess = true,
                Message = $"Password changed successfully for username: {existingUser.Username}. Your new password is {newPassword}"
            };
        }
    }
}
