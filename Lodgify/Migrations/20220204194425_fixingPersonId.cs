using Microsoft.EntityFrameworkCore.Migrations;

namespace Lodgify.Migrations
{
    public partial class fixingPersonId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookieOrder_Person_PersonId1",
                table: "CookieOrder");

            migrationBuilder.DropIndex(
                name: "IX_CookieOrder_PersonId1",
                table: "CookieOrder");

            migrationBuilder.DropColumn(
                name: "PersonId1",
                table: "CookieOrder");

            migrationBuilder.AlterColumn<long>(
                name: "PersonId",
                table: "CookieOrder",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CookieOrder_PersonId",
                table: "CookieOrder",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_CookieOrder_Person_PersonId",
                table: "CookieOrder",
                column: "PersonId",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CookieOrder_Person_PersonId",
                table: "CookieOrder");

            migrationBuilder.DropIndex(
                name: "IX_CookieOrder_PersonId",
                table: "CookieOrder");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "CookieOrder",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "PersonId1",
                table: "CookieOrder",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CookieOrder_PersonId1",
                table: "CookieOrder",
                column: "PersonId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CookieOrder_Person_PersonId1",
                table: "CookieOrder",
                column: "PersonId1",
                principalTable: "Person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
