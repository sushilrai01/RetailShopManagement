using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Suppliers.Command
{
    public class CreateSupplierCommand : IRequest<Unit>
    {
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class CreateSupplierCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider)
        : IRequestHandler<CreateSupplierCommand, Unit>
    {
        public async Task<Unit> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingSupplier = await context.Suppliers
                .Where(x => x.Name.ToLower() == request.Name.ToLower()
                            || x.Phone == request.Phone)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingSupplier != null)
            {
                throw new Exception("Another supplier already exists with the same name or phone number.");
            }

            var supplier = new Supplier()
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                CreatedBy = userServiceProvider.UserName,
                CreatedOn = DateTime.Now
            };
            await context.Suppliers.AddAsync(supplier, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
