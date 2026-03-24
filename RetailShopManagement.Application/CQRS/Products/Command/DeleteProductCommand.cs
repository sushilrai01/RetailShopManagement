
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.CQRS.Products.Command
{
    public class DeleteProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }

    public class DeleteProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        ICacheService cacheService)
        : IRequestHandler<DeleteProductCommand, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingProduct =
                await context.Products.FindAsync([request.Id], cancellationToken);

            if (existingProduct == null)
                throw new Exception($"Product not found. Id: {request.Id}");

            context.Products.Remove(existingProduct);
            await context.SaveChangesAsync(cancellationToken);

            // Invalidate all product caches
            cacheService.Remove(CacheKeyConst.AllProduct);

            return Unit.Value;
        }
    }
}
