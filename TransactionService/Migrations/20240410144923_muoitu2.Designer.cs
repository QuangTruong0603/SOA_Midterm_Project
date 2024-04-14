﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TransactionService.Data;

#nullable disable

namespace TransactionService.Migrations
{
    [DbContext(typeof(TransactionServiceContext))]
    [Migration("20240410144923_muoitu2")]
    partial class muoitu2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TransactionService.Models.OTPDetail", b =>
                {
                    b.Property<int>("OTPId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OTPId"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OTP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PaymentDetailpaymentId")
                        .HasColumnType("int");

                    b.Property<int>("paymentID")
                        .HasColumnType("int");

                    b.HasKey("OTPId");

                    b.HasIndex("PaymentDetailpaymentId");

                    b.ToTable("OTPDetail");
                });

            modelBuilder.Entity("TransactionService.Models.PaymentDetail", b =>
                {
                    b.Property<int>("paymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("paymentId"), 1L, 1);

                    b.Property<long>("amount")
                        .HasColumnType("bigint");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("paymentId");

                    b.ToTable("PaymentDetail");
                });

            modelBuilder.Entity("TransactionService.Models.OTPDetail", b =>
                {
                    b.HasOne("TransactionService.Models.PaymentDetail", null)
                        .WithMany("OTPDetails")
                        .HasForeignKey("PaymentDetailpaymentId");
                });

            modelBuilder.Entity("TransactionService.Models.PaymentDetail", b =>
                {
                    b.Navigation("OTPDetails");
                });
#pragma warning restore 612, 618
        }
    }
}