﻿// <auto-generated />
using System;
using Assignment2.Data;
using AssignmentClassLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment2.Migrations
{
    [DbContext(typeof(ModelDbContext))]
    [Migration("20220121095954_model")]
    partial class model
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Assignment2.Models.Account", b =>
                {
                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.HasKey("AccountNumber");

                    b.HasIndex("CustomerId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Assignment2.Models.BillPay", b =>
                {
                    b.Property<int>("BillPayId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BillPayId"), 1L, 1);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<int>("PayeeId")
                        .HasColumnType("int");

                    b.Property<int>("Period")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduleTimeUtc")
                        .HasColumnType("datetime2");

                    b.HasKey("BillPayId");

                    b.ToTable("BillPay");
                });

            modelBuilder.Entity("Assignment2.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Mobile")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PostCode")
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("State")
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Suburb")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("TFN")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("Assignment2.Models.Login", b =>
                {
                    b.Property<string>("LoginId")
                        .HasMaxLength(8)
                        .HasColumnType("char(8)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("char(64)");

                    b.HasKey("LoginId");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("Login");
                });

            modelBuilder.Entity("Assignment2.Models.Payee", b =>
                {
                    b.Property<int>("PayeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PayeeId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("nvarchar(14)");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("Suburb")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("PayeeId");

                    b.ToTable("Payee");
                });

            modelBuilder.Entity("Assignment2.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"), 1L, 1);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<decimal>("Amount")
                        .HasColumnType("money");

                    b.Property<string>("Comment")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int?>("DestinationAccountNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("TransactionTimeUtc")
                        .HasColumnType("datetime2");

                    b.Property<string>("TransactionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.HasKey("TransactionId");

                    b.HasIndex("AccountNumber");

                    b.HasIndex("DestinationAccountNumber");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Assignment2.Models.Account", b =>
                {
                    b.HasOne("Assignment2.Models.Customer", "Customer")
                        .WithMany("Accounts")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Assignment2.Models.Login", b =>
                {
                    b.HasOne("Assignment2.Models.Customer", "Customer")
                        .WithOne("Login")
                        .HasForeignKey("Assignment2.Models.Login", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Assignment2.Models.Transaction", b =>
                {
                    b.HasOne("Assignment2.Models.Account", "Account")
                        .WithMany("Transactions")
                        .HasForeignKey("AccountNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Assignment2.Models.Account", "DestinationAccount")
                        .WithMany()
                        .HasForeignKey("DestinationAccountNumber");

                    b.Navigation("Account");

                    b.Navigation("DestinationAccount");
                });

            modelBuilder.Entity("Assignment2.Models.Account", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Assignment2.Models.Customer", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Login")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
