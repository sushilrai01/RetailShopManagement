using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.Persistence;

namespace RetailShopManagement.Application.CQRS.Admin.Query
{
    public class GetUserByIdQuery : IRequest<UsersDto>
    {
        public Guid Id { get; set; }
    }

    public class GetUserByIdQueryHandler(IDbContextFactory<ApplicationDbContext> contextFactory)
        : IRequestHandler<GetUserByIdQuery, UsersDto>
    {
        public async Task<UsersDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var user = await context.Users
                .Where(x => x.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

            return user ?? throw new Exception($"User not found with Id: {request.Id}");
        }
    }
}
