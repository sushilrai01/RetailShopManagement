using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Suppliers.Query
{
    public class GetSupplierByIdQuery : IRequest<SupplierDto>
    {
        public int Id { get; set; }
    }

    public class GetSupplierByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetSupplierByIdQuery, SupplierDto>
    {
        public async Task<SupplierDto> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingSupplier = await context.Suppliers
                .Where(p => p.Id == request.Id)
                .AsNoTracking()
                .Select(x => new SupplierDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Address = x.Address,
                    Phone = x.Phone,
                    CreatedOn = x.CreatedOn,
                    LastModifiedOn = x.LastModifiedOn,
                }).FirstOrDefaultAsync(cancellationToken);

            return existingSupplier ?? throw new Exception($"Supplier with id {request.Id} not found.");
        }
    }
}
