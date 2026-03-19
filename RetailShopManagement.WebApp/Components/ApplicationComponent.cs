using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.WebApp.Services;
using RetailShopManagement.WebApp.Services.AppServices;
using RetailShopManagement.WebApp.Services.AppServices.Categories;
using RetailShopManagement.WebApp.Services.AppServices.ToastService;
using System.Security.Claims;
using System.Security.Principal;
using RetailShopManagement.WebApp.Services.AppServices.Creditors;

namespace RetailShopManagement.WebApp.Components
{
    public class ApplicationComponent : ComponentBase, IDisposable
    {
        [CascadingParameter]
        protected Task<AuthenticationState> AuthenticationStateTask { get; set; }
        // cached current user
        protected ClaimsPrincipal CurrentUser => AuthenticationStateTask.Result.User;

        [Inject] protected IJSRuntime Javascript { get; set; }
        [Inject] protected ICategoryService CategoryService { get; set; }
        [Inject] protected ICreditorService CreditorService { get; set; }
        [Inject] protected ToastService ToastService { get; set; }
        [Inject] protected JsModalService JsModalService { get; set; }
        protected CancellationTokenSource CancellationTokenSource { get; set; }
        protected Task<IList<IntDropDownField>> CategoryTypes => GetCategoryTypes();
        protected Task<IList<NullableGuidDropDownField>> CreditorsFieldList => GetCreditorsFieldList();

        protected bool IsAdmin()
        {
            var check = CurrentUser?.FindFirst(x => x.Type == ClaimTypes.Role)?.Value == "Admin";
            return check;

        }

        private async Task<IList<NullableGuidDropDownField>> GetCreditorsFieldList()
        {
            var response = await CreditorService.GetCreditorsAsync();

            if (response.IsSuccess)
            {
                var categoryTypes = new List<NullableGuidDropDownField>()
                {
                    new NullableGuidDropDownField() { Value = null, Text = "--Select Creditor--"}
                };

                categoryTypes.AddRange(response.Data
                    .OrderBy(x => x.FullName)
                    .Select(c => new NullableGuidDropDownField
                    {
                        Value = c.Id,
                        Text = c.FullName
                    }).ToList());

                return categoryTypes;
            }

            return new List<NullableGuidDropDownField>();
        }

        private async Task<IList<IntDropDownField>> GetCategoryTypes()
        {
            var response = await CategoryService.GetAllCategories();

            if (response.IsSuccess)
            {
                var categoryTypes = new List<IntDropDownField>()
                {
                    new IntDropDownField() { Value = 0}
                };

                categoryTypes.AddRange(response.Data
                    .OrderBy(x => x.Name)
                    .Select(c => new IntDropDownField
                    {
                        Value = c.Id,
                        Text = c.Name
                    }).ToList());

                return categoryTypes;
            }

            return new List<IntDropDownField>();
        }

        public virtual void Dispose()
        {
            CancellationTokenSource?.Cancel();
            CancellationTokenSource?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
