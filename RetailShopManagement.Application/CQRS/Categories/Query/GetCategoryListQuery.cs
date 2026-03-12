using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Categories.Query
{
    public class GetCategoryListQuery : IRequest<IList<CategoryDto>>

    {

    }

    public class GetCategoryListQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetCategoryListQuery, IList<CategoryDto>>
    {
        public async Task<IList<CategoryDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            return await context.Categories
                .AsNoTracking()
                .Select(x => new CategoryDto()
                {
                    Id = x.Id,
                    Name = x.Name,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    LastModifiedOn = x.LastModifiedOn,
                    LastModifiedBy = x.LastModifiedBy,
                })
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync(cancellationToken);

        }
    }
}
