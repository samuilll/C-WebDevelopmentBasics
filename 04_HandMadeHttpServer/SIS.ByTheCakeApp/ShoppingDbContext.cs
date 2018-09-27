using Microsoft.EntityFrameworkCore;
using SIS.ByTheCakeApp.Config;
using SIS.ByTheCakeApp.Models;

namespace SIS.ByTheCakeApp
{
    public class ShoppingDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrdersProducts { get; set; }

        public ShoppingDbContext()
        {

        }
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            :base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(Configuration.ConnectionString);
            }
            base.OnConfiguring(options);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>(e =>
            {
                e.HasKey(op => new { op.ProductId, op.OrderId });
                e
                .HasOne(op => op.Order)
                .WithMany(o=>o.OrderProducts);

                e
                .HasOne(op => op.Product)
                .WithMany(p => p.ProductOrders);
            });

            builder.Entity<User>(e =>
            {
                e
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
            });
              
            base.OnModelCreating(builder);
        }
    }
}
