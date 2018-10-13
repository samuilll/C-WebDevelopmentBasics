using Microsoft.EntityFrameworkCore;
using SIS.CakesApp.Models;

namespace SIS.CakesApp.Data
{
    public class CakesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=TheirCakes;Integrated Security=True;").UseLazyLoadingProxies();
        }
    }
}
