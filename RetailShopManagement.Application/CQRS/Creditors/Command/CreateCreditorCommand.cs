using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Domain.Constants;
using RetailShopManagement.Domain.Entities;
using RetailShopManagement.Domain.Extensions;
using RetailShopManagement.Domain.Shared;

namespace RetailShopManagement.Application.CQRS.Creditors.Command
{
    public class CreateCreditorCommand : IRequest<Guid>
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; } = null!;
        public string MobileNo { get; set; } = null!;
        public string? Email { get; set; }
        public string Address { get; set; } = null!;

        public decimal DueAmount { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public string Status { get; set; } = PaymentStatus.Pending;
    }

    public class CreateCreditorCommandHandler(IDbContextFactory<ApplicationDbContext> contextFactory,
        IUserServiceProvider userServiceProvider)
        : IRequestHandler<CreateCreditorCommand, Guid>
    {
        public async Task<Guid> Handle(CreateCreditorCommand request, CancellationToken cancellationToken)
        {
            await using var context = await contextFactory.CreateDbContextAsync(cancellationToken);

            var existingCreditor = await context.Creditors
                .Where(x => x.MobileNo.Trim().ToLower() == request.MobileNo.Trim().ToLower()).
                FirstOrDefaultAsync(cancellationToken);

            if (existingCreditor != null)
            {
                throw new Exception("Creditor with the same mobile no. already exists.");
            }

            var newId = request.Id;
            if (newId.Empty())
            {
                newId = Guid.NewGuid();
            }

            var creditor = new Creditor()
            {
                Id = newId!.Value,
                FullName = request.FullName,
                MobileNo = request.MobileNo,
                Email = request.Email,
                Address = request.Address,

                DueAmount = request.DueAmount,
                PaidAmount = request.PaidAmount,
                BalanceAmount = request.TotalAmount,
                Status = request.Status,

                CreatedBy = userServiceProvider.UserName,
                CreatedOn = DateTime.Now
            };

            #region DummyData
            //Dummy data for test

            //creditor.PaySlips = new List<PaySlip>()
            //{
            //    new PaySlip()
            //    {
            //        Id = Guid.NewGuid(),
            //        PaymentDate = DateTime.Now,
            //        AmountPaid = 250,
            //        Remarks = "paid for vegetables",
            //        CreatedBy = userServiceProvider.UserName,
            //        CreatedOn = DateTime.Now
            //    }
            //};
            #endregion

            await context.Creditors.AddAsync(creditor, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return creditor.Id;
        }
    }
}
