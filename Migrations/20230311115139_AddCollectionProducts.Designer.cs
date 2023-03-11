﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OrdersApiApp.Model;

#nullable disable

namespace OrdersApiApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230311115139_AddCollectionProducts")]
    partial class AddCollectionProducts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.OrderProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountProduct")
                        .HasColumnType("integer");

                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Article")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("OrderId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Order", b =>
                {
                    b.HasOne("OrdersApiApp.Model.Entity.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.OrderProduct", b =>
                {
                    b.HasOne("OrdersApiApp.Model.Entity.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrdersApiApp.Model.Entity.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Product", b =>
                {
                    b.HasOne("OrdersApiApp.Model.Entity.Order", null)
                        .WithMany("Products")
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Order", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("OrdersApiApp.Model.Entity.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
