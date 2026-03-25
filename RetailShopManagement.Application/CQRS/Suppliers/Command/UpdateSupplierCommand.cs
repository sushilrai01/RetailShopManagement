using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Suppliers.Command
{
    public class UpdateSupplierCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class UpdateSupplierCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider)
        : IRequestHandler<UpdateSupplierCommand, Unit>
    {
        public async Task<Unit> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var normalizedName = request.Name.Trim().ToLower();
            var normalizedPhone = request.Phone.Trim();

            var isDuplicate = await context.Suppliers
                .AnyAsync(x => x.Id != request.Id &&
                               (x.Name.ToLower() == normalizedName ||
                                x.Phone == normalizedPhone),
                    cancellationToken);

            if (isDuplicate)
            {
                throw new Exception("Another supplier already exists with the same name or phone number.");
            }

            var updateSupplier =
                await context.Suppliers.FindAsync([request.Id], cancellationToken);

            if (updateSupplier is null)
                throw new Exception($"Supplier not found with Id: {request.Id}");

            updateSupplier.Name = request.Name;
            updateSupplier.Email = request.Email;
            updateSupplier.Address = request.Address;
            updateSupplier.Phone = request.Phone;
            updateSupplier.LastModifiedBy = userServiceProvider.UserName;
            updateSupplier.LastModifiedOn = DateTime.Now;

            await context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
