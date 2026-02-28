using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RetailShopManagement.Application.Services.Products;

namespace RetailShopManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register application services, handlers, etc. here
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
