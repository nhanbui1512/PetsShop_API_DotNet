﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using petshop.Data;

#nullable disable

namespace petshop.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240625025800_addToken")]
    partial class addToken
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("PetsShop_API_DotNet.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("RoleName")
                        .HasColumnType("longtext")
                        .HasColumnName("role_name");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("petshop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("CategoryName")
                        .HasColumnType("longtext")
                        .HasColumnName("category_name");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("Description")
                        .HasColumnType("longtext")
                        .HasColumnName("description");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("petshop.Models.Option", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("Name")
                        .HasColumnType("longtext")
                        .HasColumnName("option_name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(65,30)")
                        .HasColumnName("price");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int")
                        .HasColumnName("quantity");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Options");
                });

            modelBuilder.Entity("petshop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("create_at");

                    b.Property<string>("ProductName")
                        .HasColumnType("longtext")
                        .HasColumnName("product_name");

                    b.Property<DateTime>("UpdateAt")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("update_at");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("petshop.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    b.Property<string>("AccessToken")
                        .HasColumnType("longtext")
                        .HasColumnName("access_token");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext")
                        .HasColumnName("avatar");

                    b.Property<string>("Email")
                        .HasColumnType("longtext")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext")
                        .HasColumnName("first_name");

                    b.Property<bool>("Gender")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("gender");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext")
                        .HasColumnName("last_name");

                    b.Property<string>("Password")
                        .HasColumnType("longtext")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("longtext")
                        .HasColumnName("refresh_token");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("petshop.Models.Option", b =>
                {
                    b.HasOne("petshop.Models.Product", "Product")
                        .WithMany("Options")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("petshop.Models.Product", b =>
                {
                    b.HasOne("petshop.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("petshop.Models.User", b =>
                {
                    b.HasOne("PetsShop_API_DotNet.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("PetsShop_API_DotNet.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("petshop.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("petshop.Models.Product", b =>
                {
                    b.Navigation("Options");
                });
#pragma warning restore 612, 618
        }
    }
}
