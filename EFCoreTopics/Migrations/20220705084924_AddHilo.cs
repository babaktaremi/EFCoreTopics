using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTopics.Migrations
{
    public partial class AddHilo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SpecialProductSequence",
                startValue: 10L,
                incrementBy: 5);

            migrationBuilder.CreateTable(
                name: "SpecialProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialProduct", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpecialProductPrice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecialProductId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialProductPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecialProductPrice_SpecialProduct_SpecialProductId",
                        column: x => x.SpecialProductId,
                        principalTable: "SpecialProduct",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialProductPrice_SpecialProductId",
                table: "SpecialProductPrice",
                column: "SpecialProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialProductPrice");

            migrationBuilder.DropTable(
                name: "SpecialProduct");

            migrationBuilder.DropSequence(
                name: "SpecialProductSequence");
        }
    }
}
