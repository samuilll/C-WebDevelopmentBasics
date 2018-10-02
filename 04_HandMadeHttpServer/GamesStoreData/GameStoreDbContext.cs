using GamesStoreData.Config;
using GamesStoreData.Models;
using Microsoft.EntityFrameworkCore;

namespace GamesStoreData
{
    public class GameStoreDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderGame> OrdersGames { get; set; }

        public GameStoreDbContext()
        {

        }
        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options)
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
            builder.Entity<OrderGame>(e =>
            {
                e.HasKey(og => new { og.GameId, og.OrderId });
                e
                .HasOne(og => og.Order)
                .WithMany(o=>o.Games);

                e
                .HasOne(og => og.Game)
                .WithMany(g => g.Orders);
            });

            builder.Entity<User>(e =>
            {
                e
                    .HasIndex(u => u.Email)
                    .IsUnique();

                e
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);
            });
              
            base.OnModelCreating(builder);
        }
    }
}
