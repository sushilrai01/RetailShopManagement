using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RetailShopManagement.Domain.Models.Common;
using RetailShopManagement.WebApp.Services.AppServices.Categories;

namespace RetailShopManagement.WebApp.Components
{
    public class ApplicationComponent : ComponentBase, IDisposable
    {
        [Inject] protected IJSRuntime Javascript { get; set; }
        [Inject] protected ICategoryService CategoryService { get; set; }
        protected CancellationTokenSource CancellationTokenSource { get; set; }
        protected Task<IList<IntDropDownField>> CategoryTypes => GetCategoryTypes();

        private async Task<IList<IntDropDownField>> GetCategoryTypes()
        {
            var response = await CategoryService.GetAllCategories();

            if (response.IsSuccess)
            {
                var categoryTypes = new List<IntDropDownField>()
                {
                    new IntDropDownField() { Value = 0}
                };

                categoryTypes.AddRange(response.Data.Select(c => new IntDropDownField
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
