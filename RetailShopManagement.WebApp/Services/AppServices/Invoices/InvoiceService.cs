using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Creditors.Command;
using RetailShopManagement.Application.CQRS.Creditors.Query;
using RetailShopManagement.Application.CQRS.Invoices.Command;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;
using RetailShopManagement.WebApp.Services.AppServices.Creditors;

namespace RetailShopManagement.WebApp.Services.AppServices.Invoices
{
    public class InvoiceService(IMediator mediator) : BaseService(mediator), IInvoiceService
    {
        public async Task<ApiResponse<InvoiceResponseModel>> CreateInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default)
        {
            var method = "Create Invoice";
            var apiAction = ApiAction.Create;

            try
            {
                var result = await Mediator.Send(new CreateInvoiceCommand()
                {
                    InvoiceDate = invoiceForm.InvoiceDate,
                    DueDate = invoiceForm.DueDate,
                    CreditorId = invoiceForm.CreditorId,
                    Status = invoiceForm.Status,
                    SubTotal = invoiceForm.SubTotal,
                    TaxAmount = invoiceForm.TaxAmount,
                    TaxRate = invoiceForm.TaxRate,
                    DiscountAmount = invoiceForm.DiscountAmount,
                    DiscountPercent = invoiceForm.DiscountPercent,
                    TotalAmount = invoiceForm.TotalAmount,
                    PaidAmount = invoiceForm.PaidAmount,
                    BalanceAmount = invoiceForm.BalanceAmount,
                    Remarks = invoiceForm.Remarks,
                        
                    InvoiceItems = invoiceForm.InvoiceItems
                }, cancellationToken);

                return new ApiResponse<InvoiceResponseModel>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<InvoiceResponseModel>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }
        }
    }
}
