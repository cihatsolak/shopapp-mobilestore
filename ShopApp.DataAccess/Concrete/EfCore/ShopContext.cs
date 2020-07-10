using Microsoft.EntityFrameworkCore;
using ShopApp.Core.Domain.Carts;
using ShopApp.Core.Domain.Categories;
using ShopApp.Core.Domain.Orders;
using ShopApp.Core.Domain.Products;
using ShopApp.Core.Domain.Relationships;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class ShopContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=HPPROBOOK;Database=ShopDb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasKey(c => new { c.CategoryId, c.ProductId });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}