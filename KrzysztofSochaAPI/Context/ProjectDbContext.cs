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
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<OrderClothes> OrderClothes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Order>()
                .Property(o => o.DeliveryPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.ClothesAmount)
                .HasColumnType("decimal(18,2)");

            
            modelBuilder.Entity<Clothes>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)");
            //Relacja wiele do wielu z użyciem encji słabej tworząca koszyk użytkownika
            modelBuilder.Entity<ShoppingCartItem>()
          .HasKey(sc => new { sc.ClothesId, sc.UserId });
            modelBuilder.Entity<ShoppingCartItem>()
                .HasOne(s => s.Clothes)
                .WithMany(c => c.UserBuyers)
                .HasForeignKey(sc => sc.ClothesId);
            modelBuilder.Entity<ShoppingCartItem>()
               .HasOne(s => s.User)
               .WithMany(u => u.ClothesToBuy)
               .HasForeignKey(sc => sc.UserId);
            //Lista zamówionych ubrań
            modelBuilder.Entity<OrderClothes>()
            .HasKey(oc => new { oc.OrderId, oc.OrderedClothesId });
            modelBuilder.Entity<OrderClothes>()
                .HasOne(oc => oc.OrderedClothes)
                .WithMany(c => c.Orders)
                .HasForeignKey(oc => oc.OrderedClothesId);
            modelBuilder.Entity<OrderClothes>()
               .HasOne(oc => oc.Order)
               .WithMany(o => o.OrderedClothes)
               .HasForeignKey(oc => oc.OrderId);






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
