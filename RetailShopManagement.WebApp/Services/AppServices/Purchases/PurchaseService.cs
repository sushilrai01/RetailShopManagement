using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Invoices.Command;
using RetailShopManagement.Application.CQRS.Products.Query;
using RetailShopManagement.Application.CQRS.Purchases.Query;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

namespace RetailShopManagement.WebApp.Services.AppServices.Purchases
{
    public class PurchaseService(IMediator mediator) : BaseService(mediator), IPurchaseService
    {
        public async Task<ApiResponse<InvoiceResponseModel>> CreatePurchaseOrderInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default)
        {
            var method = "Create Purchase Order";
            var apiAction = ApiAction.Create;

            try
            {
                var result = await Mediator.Send(new CreatePurchaseOrderInvoiceCommand()
                {
                    InvoiceDate = invoiceForm.InvoiceDate,
                    DueDate = invoiceForm.DueDate,
                    SupplierId = invoiceForm.SupplierId!.Value,
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

                    PurchaseItems = invoiceForm.PurchaseItems
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
        public async Task<ApiResponse<InvoiceResponseModel>> UpdatePurchaseOrderInvoiceAsync(InvoicesDto invoiceForm, CancellationToken cancellationToken = default)
        {
            var method = "Update Purchase Order";
            var apiAction = ApiAction.Create;

            try
            {
                var result = await Mediator.Send(new UpdatePurchaseOrderInvoiceCommand()
                {
                    Id = invoiceForm.Id,
                    InvoiceDate = invoiceForm.InvoiceDate,
                    DueDate = invoiceForm.DueDate,
                    SupplierId = invoiceForm.SupplierId!.Value,
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

                    PurchaseItems = invoiceForm.PurchaseItems
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

        public async Task<ApiResponse<ProductPurchaseInfoDto>> GetProductPurchaseHistoryAsync(int supplierId, string orderStatus = "All", DateTime? fromDate = null, DateTime? toDate = null,
            CancellationToken ct = default)
        { 
            var method = "Get Product purchase";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetProductPurchaseListQuery()
                {
                    SupplierId = supplierId,
                    OrderStatus = orderStatus,
                    FromDate = fromDate,
                    ToDate = toDate
                }, ct);

                return new ApiResponse<ProductPurchaseInfoDto>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<ProductPurchaseInfoDto>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Failed",
                };
            }
        }
    }
}
