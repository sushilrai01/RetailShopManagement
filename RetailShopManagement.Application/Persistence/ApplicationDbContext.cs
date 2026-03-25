using Microsoft.EntityFrameworkCore;
using RetailShopManagement.Domain.Entities;

namespace RetailShopManagement.Application.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        //public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Creditor> Creditors { get; set; } = null!;
        public DbSet<PaySlip> PaySlips { get; set; } = null!;
        public DbSet<Invoice> Invoices { get; set; } = null!;
        public DbSet<ProductSale> ProductSales { get; set; } = null!;
        public DbSet<ProductPurchase> ProductPurchases { get; set; } = null!;
        public DbSet<InventoryItem> InventoryItems { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;


    }
}
