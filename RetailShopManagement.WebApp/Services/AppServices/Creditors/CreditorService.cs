using MediatR;
using RetailShopManagement.Application.Common.Models;
using RetailShopManagement.Application.CQRS.Categories.Command;
using RetailShopManagement.Application.CQRS.Categories.Query;
using RetailShopManagement.Application.CQRS.Creditors.Command;
using RetailShopManagement.Application.CQRS.Creditors.Query;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.Domain.Shared.Messages;

namespace RetailShopManagement.WebApp.Services.AppServices.Creditors
{
    public class CreditorService(IMediator mediator) : BaseService(mediator), ICreditorService
    {
        public async Task<ApiResponse<IList<CreditorDto>>> GetCreditorsAsync(CancellationToken cancellationToken = default)
        {
            var method = "Get all creditors";
            var apiAction = ApiAction.Fetch;

            try
            {
                var result = await Mediator.Send(new GetCreditorsListQuery() { }, cancellationToken);

                return new ApiResponse<IList<CreditorDto>>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<IList<CreditorDto>>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }

        }

        public async Task<ApiResponse<Guid>> CreateCreditorAsync(CreditorDto creditorModel, CancellationToken cancellationToken = default)
        {
            var method = "Create Creditor";
            var apiAction = ApiAction.Create;

            try
            {
                var result = await Mediator.Send(new CreateCreditorCommand()
                {
                    Id = creditorModel.Id,
                    Address = creditorModel.Address,
                    DueAmount = creditorModel.DueAmount,
                    Email = creditorModel.Email,
                    FullName = creditorModel.FullName,
                    PaidAmount = creditorModel.PaidAmount,
                    TotalAmount = creditorModel.TotalAmount,
                    MobileNo = creditorModel.MobileNo,
                    Status = creditorModel.Status
                }, cancellationToken);

                return new ApiResponse<Guid>()
                {
                    Message = ReturnMessage.Success(method, apiAction),
                    IsSuccess = true,
                    Title = $"{method} Success",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<Guid>()
                {
                    Message = ex.InnerException?.Message ?? ex.Message,
                    IsSuccess = false,
                    Title = $"{method} Success",
                };
            }
        }
    }
}
