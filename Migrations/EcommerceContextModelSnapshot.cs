﻿// <auto-generated />
using System;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecommerce.Migrations
{
    [DbContext(typeof(EcommerceContext))]
    partial class EcommerceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Ecommerce.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Ecommerce.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParrentId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ParrentId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Ecommerce.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Ecommerce.Models.InvoiceDetails", b =>
                {
                    b.Property<int>("InvoiceId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("InvoiceId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("InvoiceDetails");
                });

            modelBuilder.Entity("Ecommerce.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Featured")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Photo");
                });

            modelBuilder.Entity("Ecommerce.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Featured")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Ecommerce.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Ecommerce.Models.RoleAccount", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccountId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoleId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("RoleAccount");
                });

            modelBuilder.Entity("Ecommerce.Models.SlideShow", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SlideShow");
                });

            modelBuilder.Entity("Ecommerce.Models.Category", b =>
                {
                    b.HasOne("Ecommerce.Models.Category", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParrentId")
                        .HasConstraintName("FK_Category_Category");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Ecommerce.Models.Invoice", b =>
                {
                    b.HasOne("Ecommerce.Models.Account", "Account")
                        .WithMany("Invoices")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Category_Product")
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("Ecommerce.Models.InvoiceDetails", b =>
                {
                    b.HasOne("Ecommerce.Models.Invoice", "Invoice")
                        .WithMany("InvoiceDetailses")
                        .HasForeignKey("InvoiceId")
                        .HasConstraintName("FK_InvoiceDetails_Invoice")
                        .IsRequired();

                    b.HasOne("Ecommerce.Models.Product", "Product")
                        .WithMany("InvoiceDetailses")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_InvoiceDetails_Product")
                        .IsRequired();

                    b.Navigation("Invoice");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Models.Photo", b =>
                {
                    b.HasOne("Ecommerce.Models.Product", "Product")
                        .WithMany("Photos")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Product_Photo")
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Ecommerce.Models.Product", b =>
                {
                    b.HasOne("Ecommerce.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK_Category_Product")
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Ecommerce.Models.RoleAccount", b =>
                {
                    b.HasOne("Ecommerce.Models.Account", "Account")
                        .WithMany("RoleAccounts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Account_Role_Account")
                        .IsRequired();

                    b.HasOne("Ecommerce.Models.Role", "Role")
                        .WithMany("RoleAccounts")
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_Account_Role_Role")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Ecommerce.Models.Account", b =>
                {
                    b.Navigation("Invoices");

                    b.Navigation("RoleAccounts");
                });

            modelBuilder.Entity("Ecommerce.Models.Category", b =>
                {
                    b.Navigation("InverseParent");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Ecommerce.Models.Invoice", b =>
                {
                    b.Navigation("InvoiceDetailses");
                });

            modelBuilder.Entity("Ecommerce.Models.Product", b =>
                {
                    b.Navigation("InvoiceDetailses");

                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Ecommerce.Models.Role", b =>
                {
                    b.Navigation("RoleAccounts");
                });
#pragma warning restore 612, 618
        }
    }
}
