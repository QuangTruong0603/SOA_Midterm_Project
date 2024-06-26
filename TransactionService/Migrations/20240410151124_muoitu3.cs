﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionService.Migrations
{
    public partial class muoitu3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTPDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PaymentDetail",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PaymentDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OTP",
                table: "PaymentDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "PaymentDetail");

            migrationBuilder.DropColumn(
                name: "OTP",
                table: "PaymentDetail");

            migrationBuilder.CreateTable(
                name: "OTPDetail",
                columns: table => new
                {
                    OTPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDetailpaymentId = table.Column<int>(type: "int", nullable: true),
                    paymentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTPDetail", x => x.OTPId);
                    table.ForeignKey(
                        name: "FK_OTPDetail_PaymentDetail_PaymentDetailpaymentId",
                        column: x => x.PaymentDetailpaymentId,
                        principalTable: "PaymentDetail",
                        principalColumn: "paymentId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OTPDetail_PaymentDetailpaymentId",
                table: "OTPDetail",
                column: "PaymentDetailpaymentId");
        }
    }
}
