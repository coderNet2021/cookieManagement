using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Lodgify.Migrations
{
    public partial class FinalMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CookieType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookieType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "CookieOrder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookieOrder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CookieOrder_Person_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CookieTypeId = table.Column<int>(type: "int", nullable: false),
                    CookieOrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_CookieOrder_CookieOrderId",
                        column: x => x.CookieOrderId,
                        principalTable: "CookieOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderDetails_CookieType_CookieTypeId",
                        column: x => x.CookieTypeId,
                        principalTable: "CookieType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CookieOrder_PersonId",
                table: "CookieOrder",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_CookieTypePriceList_CookieTypeId",
                table: "CookieTypePriceList",
                column: "CookieTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CookieOrderId",
                table: "OrderDetails",
                column: "CookieOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_CookieTypeId",
                table: "OrderDetails",
                column: "CookieTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CookieTypePriceList");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "CookieOrder");

            migrationBuilder.DropTable(
                name: "CookieType");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
