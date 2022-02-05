using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lodgify.Migrations
{
    public partial class RefactoringCookieTypeAndCookieTypePriceList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AtDate",
                table: "CookieType");

            migrationBuilder.CreateTable(
                name: "CookieTypePriceList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<double>(type: "float", nullable: false),
                    AtDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CookieTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookieTypePriceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookieTypePriceList_CookieType_CookieTypeId",
                        column: x => x.CookieTypeId,
                        principalTable: "CookieType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookieTypePriceList_CookieTypeId",
                table: "CookieTypePriceList",
                column: "CookieTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookieTypePriceList");

            migrationBuilder.AddColumn<DateTime>(
                name: "AtDate",
                table: "CookieType",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
