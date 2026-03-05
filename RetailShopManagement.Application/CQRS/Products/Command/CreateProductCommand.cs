using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.CQRS.Products.Command
{
    public class CreateProductCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateProductCommandHandler( IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<CreateProductCommand, Guid>
    {
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                Unit = request.Unit,
                CategoryId = request.CategoryId,
                CreatedBy = "Sushil Rai",
                CreatedOn = DateTime.UtcNow
            };
            await context.Products.AddAsync(product, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}
