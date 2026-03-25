using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Suppliers.Query
{
    public class GetSupplierListQuery : IRequest<IList<SupplierDto>>
    {

    }

    public class GetSupplierListQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetSupplierListQuery, IList<SupplierDto>>
    {
        public async Task<IList<SupplierDto>> Handle(GetSupplierListQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            return await context.Suppliers
                .AsNoTracking()
                .Select(x => new SupplierDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Phone = x.Phone,
                    Email = x.Email,
                    Address = x.Address,
                    CreatedOn = x.CreatedOn,
                    LastModifiedOn = x.LastModifiedOn,
                })
                .OrderByDescending(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
