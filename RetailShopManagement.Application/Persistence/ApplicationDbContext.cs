using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        
    }
}
