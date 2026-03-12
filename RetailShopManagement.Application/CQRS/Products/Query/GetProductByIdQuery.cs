using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetailShopManagement.Application.CQRS.Products.Query
{
    public class GetProductByIdQuery : IRequest<ProductDto>

    {
        public Guid Id { get; set; }
    }

    public class GetProductByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingProduct = await context.Products
                .Where(p => p.Id == request.Id)
                .AsNoTracking()
                .Select(x => new ProductDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    Unit = x.Unit,
                    CreatedOn = x.CreatedOn,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            return existingProduct ?? new ProductDto();
        }
    }
}
