using MediatR;

namespace RetailShopManagement.WebApp.Services.AppServices
{
    public class BaseService
    {
        public readonly IMediator Mediator;

        public BaseService(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
