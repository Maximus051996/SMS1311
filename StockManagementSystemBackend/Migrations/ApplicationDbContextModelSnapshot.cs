﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockManagementSystemBackend.Data;

#nullable disable

namespace StockManagementSystemBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StockManagementSystemBackend.Models.CompanyMaster", b =>
                {
                    b.Property<long>("CompanyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("CompanyId"), 1L, 1);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Priroty")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("CompanyId");

                    b.HasIndex("CompanyName")
                        .IsUnique();

                    b.HasIndex("TenantId");

                    b.ToTable("CompanyMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.ProductMaster", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"), 1L, 1);

                    b.Property<long?>("CompantyId")
                        .IsRequired()
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Formula")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<decimal?>("MRP")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Percentage")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long?>("Quantity")
                        .HasColumnType("bigint");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("CompantyId");

                    b.HasIndex("ProductName")
                        .IsUnique();

                    b.HasIndex("TenantId");

                    b.ToTable("ProductMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.RoleMaster", b =>
                {
                    b.Property<long>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("RoleId"), 1L, 1);

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("RoleId");

                    b.HasIndex("TenantId");

                    b.HasIndex("RoleName", "TenantId")
                        .IsUnique();

                    b.ToTable("RoleMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.TenantMaster", b =>
                {
                    b.Property<int>("TenantId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantId"), 1L, 1);

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("TenantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("TenantId");

                    b.HasIndex("TenantName")
                        .IsUnique();

                    b.ToTable("TenantMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.UserMaster", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ContactNumber")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("IsUpdated")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("ContactNumber")
                        .IsUnique()
                        .HasFilter("[ContactNumber] IS NOT NULL");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserName")
                        .IsUnique()
                        .HasFilter("[UserName] IS NOT NULL");

                    b.ToTable("UserMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.CompanyMaster", b =>
                {
                    b.HasOne("StockManagementSystemBackend.Models.TenantMaster", "TenantMaster")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TenantMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.ProductMaster", b =>
                {
                    b.HasOne("StockManagementSystemBackend.Models.CompanyMaster", "CompanyMaster")
                        .WithMany()
                        .HasForeignKey("CompantyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockManagementSystemBackend.Models.TenantMaster", "TenantMaster")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CompanyMaster");

                    b.Navigation("TenantMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.RoleMaster", b =>
                {
                    b.HasOne("StockManagementSystemBackend.Models.TenantMaster", "TenantMaster")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TenantMaster");
                });

            modelBuilder.Entity("StockManagementSystemBackend.Models.UserMaster", b =>
                {
                    b.HasOne("StockManagementSystemBackend.Models.RoleMaster", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("StockManagementSystemBackend.Models.TenantMaster", "TenantMaster")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("TenantMaster");
                });
#pragma warning restore 612, 618
        }
    }
}
