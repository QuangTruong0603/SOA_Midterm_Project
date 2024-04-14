using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionService.Migrations
{
    public partial class muoitu1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PaymentDetail",
                columns: table => new
                {
                    paymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    amount = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentDetail", x => x.paymentId);
                });

            migrationBuilder.CreateTable(
                name: "OTPDetail",
                columns: table => new
                {
                    OTPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OTP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentID = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentDetailpaymentId = table.Column<int>(type: "int", nullable: true)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTPDetail");

            migrationBuilder.DropTable(
                name: "PaymentDetail");
        }
    }
}
