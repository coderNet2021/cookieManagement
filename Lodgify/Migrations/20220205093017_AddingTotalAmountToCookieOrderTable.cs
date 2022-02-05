using Microsoft.EntityFrameworkCore.Migrations;

namespace Lodgify.Migrations
{
    public partial class AddingTotalAmountToCookieOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalAmount",
                table: "CookieOrder",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "CookieOrder");
        }
    }
}
