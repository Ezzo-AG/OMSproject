﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OMSproject.Data;

#nullable disable

namespace OMSproject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OMSproject.Models.Client", b =>
                {
                    b.Property<int?>("Client_id")
                        .HasColumnType("int");

                    b.Property<string>("Claasification")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Client_id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("OMSproject.Models.Color", b =>
                {
                    b.Property<int>("Product_Id")
                        .HasColumnType("int");

                    b.Property<string>("ColorName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Product_Id", "ColorName");

                    b.ToTable("colors");
                });

            modelBuilder.Entity("OMSproject.Models.Order", b =>
                {
                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Client_id")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOFOrder")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("OrderStatus")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("SellPrice")
                        .HasColumnType("real");

                    b.Property<float>("Total_price")
                        .HasColumnType("real");

                    b.HasKey("OrderId");

                    b.HasIndex("Client_id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OMSproject.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("ClrName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("SubQty")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "ProductId", "ClrName");

                    b.HasIndex("ProductId");

                    b.ToTable("Orderdetails");
                });

            modelBuilder.Entity("OMSproject.Models.Product", b =>
                {
                    b.Property<int?>("Product_Id")
                        .HasColumnType("int");

                    b.Property<float>("Cost")
                        .HasColumnType("real");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(250)");

                    b.Property<float?>("Price")
                        .IsRequired()
                        .HasColumnType("real");

                    b.Property<string>("Product_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Total_QTY")
                        .HasColumnType("int");

                    b.HasKey("Product_Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OMSproject.Models.Color", b =>
                {
                    b.HasOne("OMSproject.Models.Product", "Product")
                        .WithMany("colors")
                        .HasForeignKey("Product_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OMSproject.Models.Order", b =>
                {
                    b.HasOne("OMSproject.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("Client_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("OMSproject.Models.OrderDetails", b =>
                {
                    b.HasOne("OMSproject.Models.Order", "Orders")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OMSproject.Models.Product", "Products")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Orders");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("OMSproject.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("OMSproject.Models.Product", b =>
                {
                    b.Navigation("OrderDetails");

                    b.Navigation("colors");
                });
#pragma warning restore 612, 618
        }
    }
}
