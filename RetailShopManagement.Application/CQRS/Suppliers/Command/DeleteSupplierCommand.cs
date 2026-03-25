using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;

namespace RetailShopManagement.Application.CQRS.Suppliers.Command
{
    public class DeleteSupplierCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }

    public class DeleteSupplierCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<DeleteSupplierCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingProduct =
                await context.Suppliers.FindAsync([request.Id], cancellationToken);

            if (existingProduct == null)
                throw new Exception($"Supplier not found. Id: {request.Id}");

            context.Suppliers.Remove(existingProduct);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
