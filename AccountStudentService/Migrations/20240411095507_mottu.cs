using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountStudentService.Migrations
{
    public partial class mottu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "beneId",
                table: "HistoryTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "beneName",
                table: "HistoryTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "beneId",
                table: "HistoryTransactions");

            migrationBuilder.DropColumn(
                name: "beneName",
                table: "HistoryTransactions");
        }
    }
}
