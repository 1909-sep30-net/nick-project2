using System;
using KitchenRestService.Logic;
using Microsoft.EntityFrameworkCore;

namespace KitchenRestService.Data
{
    public class KitchenContext : DbContext
    {
        public KitchenContext(DbContextOptions<KitchenContext> options) : base(options)
        {
        }

        public KitchenContext()
        {
        }

        public DbSet<FridgeItem> FridgeItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FridgeItem>(entity =>
            {
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                    .UseIdentityColumn();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(128);

                // unique
                entity.HasAlternateKey(e => e.Email);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);
            });
        }
    }
}
