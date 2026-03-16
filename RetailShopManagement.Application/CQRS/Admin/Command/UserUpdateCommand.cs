using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Admin.Command
{
    public class UserUpdateCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string MobileNo { get; set; } = null!;

        public string Address { get; set; } = null!;
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }

    public class UserUpdateCommandHandler(
        IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider,
        IPasswordHasher hasher)
        : IRequestHandler<UserUpdateCommand, Unit>
    {
        public async Task<Unit> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingUser = await context.Users
                .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);

            if (existingUser is null)
            {
                throw new Exception($"User not found with Id: {request.Id}");
            }

            existingUser.FullName = request.FullName;
            existingUser.Email = request.Email;
            existingUser.Username = string.IsNullOrWhiteSpace(existingUser.Username) ? request.Username : existingUser.Username;
            existingUser.MobileNo = request.MobileNo;
            existingUser.Address = request.Address;
            existingUser.IsActive = request.IsActive;
            existingUser.Role = request.Role;
            existingUser.LastModifiedBy = userServiceProvider.UserName;
            existingUser.LastModifiedOn = DateTime.Now;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
