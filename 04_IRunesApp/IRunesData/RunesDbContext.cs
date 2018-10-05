using System.Collections.Generic;
using IRunes.Domain.Models;
using IRunesData.Utilities;
using Microsoft.EntityFrameworkCore;

namespace IRunesData
{
    public class RunesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<AlbumTrack> AlbumsTracks { get; set; }


        public RunesDbContext()
        {
                
        }
        public RunesDbContext(DbContextOptions<RunesDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(DataConstants.ConnectionString).UseLazyLoadingProxies();
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AlbumTrack>(e =>
                {
                    e.HasKey(at => new {at.AlbumId, at.TrackId});

                    e.HasOne(at => at.Album)
                        .WithMany(a => a.AlbumTracks)
                        .HasForeignKey(at => at.AlbumId);

                    e.HasOne(at => at.Track)
                        .WithMany(t => t.TrackAlbums)
                        .HasForeignKey(at => at.TrackId);
                }
            );

            builder.Entity<User>(e =>
            {
                e.HasAlternateKey(u => u.Email);
                e.HasAlternateKey(u => u.Username);
            });

            base.OnModelCreating(builder);
        }
    }
}
