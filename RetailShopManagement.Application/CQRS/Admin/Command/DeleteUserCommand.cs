
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Admin.Command
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteUserCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<DeleteUserCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingUser =
                await context.Users.FindAsync([request.Id], cancellationToken);

            if (existingUser == null)
                throw new Exception($"User not found. Id: {request.Id}");

            context.Users.Remove(existingUser);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
