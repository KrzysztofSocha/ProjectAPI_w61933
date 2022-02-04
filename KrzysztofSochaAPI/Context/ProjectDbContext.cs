using KrzysztofSochaAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KrzysztofSochaAPI.Context
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {

        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Shop> Shops { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .Property(p => p.DeliveryPrice)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
                .Property(p => p.ClothesAmount)
                .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Clothes>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Clothes>()
                .HasMany(c => c.Orders)
                .WithMany(o => o.OrderedClothes)
                .UsingEntity(o => o.ToTable("OrderedClothes"));

            modelBuilder.Entity<Shop>()
              .HasOne(c => c.Manager)
              .WithOne(o => o.Shop);

            modelBuilder.Entity<Order>()
             .HasOne(c => c.DeliveryAddress)
             .WithOne(o => o.Order);

            modelBuilder.Entity<User>()
             .HasOne(c => c.Address)
             .WithOne(o => o.User);
        }
    }
}
