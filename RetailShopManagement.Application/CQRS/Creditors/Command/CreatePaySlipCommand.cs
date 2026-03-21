using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Creditors.Command
{
    public class CreatePaySlipCommand : IRequest<Guid>
    {
        public Guid CreditorId { get; set; }
        public string Remarks { get; set; }
        public decimal AmountPaid { get; set; }

    }

    public class CreatePaySlipCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider)
        : IRequestHandler<CreatePaySlipCommand, Guid>
    {
        public async Task<Guid> Handle(CreatePaySlipCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingCreditor = await context.Creditors
                .Where(x => x.Id == request.CreditorId).
                FirstOrDefaultAsync(cancellationToken);

            if (existingCreditor == null)
            {
                throw new Exception($"Creditor does not exist with Id: {request.CreditorId}.");
            }

            var paySlip = new PaySlip()
            {
                CreditorId = request.CreditorId,
                PaymentDate = DateTime.Now,
                AmountPaid = request.AmountPaid,
                Remarks = request.Remarks,

                CreatedBy = userServiceProvider.UserName,
                CreatedOn = DateTime.Now
            };


            await context.PaySlips.AddAsync(paySlip, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return paySlip.Id;
        }
    }
}
