using RetailShopManagement.Domain.Shared;
using RetailShopManagement.WebApp.Services;
using RetailShopManagement.WebApp.Services.AppServices.AuthServices;
using RetailShopManagement.WebApp.Services.AppServices.Categories;
using RetailShopManagement.WebApp.Services.AppServices.Creditors;
using RetailShopManagement.WebApp.Services.AppServices.Inventory;
using RetailShopManagement.WebApp.Services.AppServices.Invoices;
using RetailShopManagement.WebApp.Services.AppServices.Products;
using RetailShopManagement.WebApp.Services.AppServices.Reports;
using RetailShopManagement.WebApp.Services.AppServices.Suppliers;

namespace RetailShopManagement.WebApp.Extensions
{
    public static class ApplicationModuleExtension
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICreditorService, CreditorService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IInventoryService, InventoryService>();

            services.AddSingleton<IUserServiceProvider, UserServiceProvider>();

        }
    }
}
