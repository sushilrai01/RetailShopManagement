using RetailShopManagement.WebApp.Services.AppServices.Categories;
using RetailShopManagement.WebApp.Services.AppServices.Products;

namespace RetailShopManagement.WebApp.Extensions
{
    public static class ApplicationModuleExtension
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

        }
    }
}
