using Microsoft.EntityFrameworkCore.Migrations;

namespace Lodgify.Migrations
{
    public partial class RefactoringCookieOrderInOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_CookieOrder_CookieOrderId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CookieOrderId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_CookieOrder_CookieOrderId",
                table: "OrderDetails",
                column: "CookieOrderId",
                principalTable: "CookieOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_CookieOrder_CookieOrderId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<int>(
                name: "CookieOrderId",
                table: "OrderDetails",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_CookieOrder_CookieOrderId",
                table: "OrderDetails",
                column: "CookieOrderId",
                principalTable: "CookieOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
