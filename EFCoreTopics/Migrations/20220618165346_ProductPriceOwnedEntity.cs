using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTopics.Migrations
{
    public partial class ProductPriceOwnedEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "ProductPrices");

            migrationBuilder.AddColumn<int>(
                name: "Money_Unit",
                table: "ProductPrices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Money_Value",
                table: "ProductPrices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money_Unit",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "Money_Value",
                table: "ProductPrices");

            migrationBuilder.AddColumn<string>(
                name: "Money",
                table: "ProductPrices",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
