using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Admin.Query
{
    public class GetUsersListQuery : IRequest<IList<UsersDto>>
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class GetUsersListQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetUsersListQuery, IList<UsersDto>>
    {
        public async Task<IList<UsersDto>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var fromDate = request.FromDate ?? DateTime.Now.Date.AddDays(-14);
            var toDate = request.ToDate ?? DateTime.Now;

            return await context.Users
                .Where(x => (x.CreatedOn >= fromDate &&
                                x.CreatedOn < toDate.Date.AddDays(1)))
                .AsNoTracking()
                .Select(x => new UsersDto()
                {
                    Id = x.Id,
                    Email = x.Email,
                    FullName = x.FullName,
                    Address = x.Address,
                    IsActive = x.IsActive,
                    MobileNo = x.MobileNo,
                    Role = x.Role,
                    Username = x.Username,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    LastModifiedBy = x.LastModifiedBy,
                    LastModifiedOn = x.LastModifiedOn
                })
                .OrderByDescending(x => x.CreatedOn)
                .ThenBy(x => !x.IsActive)
                .ToListAsync(cancellationToken);

            //return await productService.GetAllProductsAsync();
        }
    }
}
