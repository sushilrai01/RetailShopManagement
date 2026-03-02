using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RetailShopManagement.Application.Persistence;
using RetailShopManagement.Infrastructure.Persistence;
//using RetailShopManagement.Domain.RepositoryInterfaces;
//using RetailShopManagement.Infrastructure.Repositories;

namespace RetailShopManagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register application services, handlers, etc. here
            //services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            //});

            //services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
