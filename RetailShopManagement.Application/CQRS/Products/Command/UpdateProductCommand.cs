
using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Helpers;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.CQRS.Products.Command
{
    public class UpdateProductCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public int CategoryId { get; set; }
    }

    public class UpdateProductCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        ICacheService cacheService)
        : IRequestHandler<UpdateProductCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingProduct =
                await context.Products.FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (existingProduct is null)
                throw new Exception($"Product doesn't exist with Id: {request.Id}");

            existingProduct.Name = request.Name;
            existingProduct.Description = request.Description;
            existingProduct.Price = request.Price;
            existingProduct.Quantity = request.Quantity;
            existingProduct.Unit = request.Unit;
            existingProduct.CategoryId = request.CategoryId;
            existingProduct.LastModifiedBy = "Sushil Rai";
            existingProduct.LastModifiedOn = DateTime.Now;

            await context.SaveChangesAsync(cancellationToken);

            // Invalidate all product caches
            cacheService.Remove(CacheKeyConst.AllProduct);

            return Unit.Value;
        }
    }
}
