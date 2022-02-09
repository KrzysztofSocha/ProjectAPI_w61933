﻿// <auto-generated />
using System;
using KrzysztofSochaAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KrzysztofSochaAPI.Migrations
{
    [DbContext(typeof(ProjectDbContext))]
    [Migration("20220209183938_AddPaymentStatusProperty")]
    partial class AddPaymentStatusProperty
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApartamentNumber")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("HouseNumber")
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<string>("Street")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Clothes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsAvailability")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Clothes");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Delivery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Deliveries");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClothesId")
                        .HasColumnType("int");

                    b.Property<byte[]>("ImageFile")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClothesId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeliveryAddressId")
                        .HasColumnType("int");

                    b.Property<int>("DeliveryId")
                        .HasColumnType("int");

                    b.Property<bool>("FreeDelivery")
                        .HasColumnType("bit");

                    b.Property<int>("PaymentId")
                        .HasColumnType("int");

                    b.Property<int>("PurchaserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReceivedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryAddressId")
                        .IsUnique();

                    b.HasIndex("DeliveryId")
                        .IsUnique();

                    b.HasIndex("PaymentId")
                        .IsUnique();

                    b.HasIndex("PurchaserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.OrderClothes", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("OrderedClothesId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "OrderedClothesId");

                    b.HasIndex("OrderedClothesId");

                    b.ToTable("OrderClothes");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("ManagerId")
                        .IsUnique();

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.ShoppingCartItem", b =>
                {
                    b.Property<int>("ClothesId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("ClothesId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeleterUserId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ModifierUserId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Image", b =>
                {
                    b.HasOne("KrzysztofSochaAPI.Models.Clothes", "Clothes")
                        .WithMany("Images")
                        .HasForeignKey("ClothesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothes");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Order", b =>
                {
                    b.HasOne("KrzysztofSochaAPI.Models.Address", "DeliveryAddress")
                        .WithOne("Order")
                        .HasForeignKey("KrzysztofSochaAPI.Models.Order", "DeliveryAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KrzysztofSochaAPI.Models.Delivery", "Delivery")
                        .WithOne("Order")
                        .HasForeignKey("KrzysztofSochaAPI.Models.Order", "DeliveryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KrzysztofSochaAPI.Models.Payment", "Payment")
                        .WithOne("Order")
                        .HasForeignKey("KrzysztofSochaAPI.Models.Order", "PaymentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KrzysztofSochaAPI.Models.User", "Purchaser")
                        .WithMany()
                        .HasForeignKey("PurchaserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Delivery");

                    b.Navigation("DeliveryAddress");

                    b.Navigation("Payment");

                    b.Navigation("Purchaser");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.OrderClothes", b =>
                {
                    b.HasOne("KrzysztofSochaAPI.Models.Order", "Order")
                        .WithMany("OrderedClothes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KrzysztofSochaAPI.Models.Clothes", "OrderedClothes")
                        .WithMany("Orders")
                        .HasForeignKey("OrderedClothesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("OrderedClothes");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Shop", b =>
                {
                    b.HasOne("KrzysztofSochaAPI.Models.Address", "Address")
                        .WithOne("Shop")
                        .HasForeignKey("KrzysztofSochaAPI.Models.Shop", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KrzysztofSochaAPI.Models.User", "Manager")
                        .WithOne("Shop")
                        .HasForeignKey("KrzysztofSochaAPI.Models.Shop", "ManagerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.ShoppingCartItem", b =>
                {
                    b.HasOne("KrzysztofSochaAPI.Models.Clothes", "Clothes")
                        .WithMany("UserBuyers")
                        .HasForeignKey("ClothesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KrzysztofSochaAPI.Models.User", "User")
                        .WithMany("ClothesToBuy")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Clothes");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.User", b =>
                {
                    b.HasOne("KrzysztofSochaAPI.Models.Address", "Address")
                        .WithOne("User")
                        .HasForeignKey("KrzysztofSochaAPI.Models.User", "AddressId");

                    b.HasOne("KrzysztofSochaAPI.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Address", b =>
                {
                    b.Navigation("Order");

                    b.Navigation("Shop");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Clothes", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Orders");

                    b.Navigation("UserBuyers");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Delivery", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Order", b =>
                {
                    b.Navigation("OrderedClothes");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.Payment", b =>
                {
                    b.Navigation("Order");
                });

            modelBuilder.Entity("KrzysztofSochaAPI.Models.User", b =>
                {
                    b.Navigation("ClothesToBuy");

                    b.Navigation("Shop");
                });
#pragma warning restore 612, 618
        }
    }
}
