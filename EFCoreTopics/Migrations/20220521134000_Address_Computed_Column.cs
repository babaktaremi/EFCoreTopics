using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTopics.Migrations
{
    public partial class Address_Computed_Column : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SearchTerm",
                schema: "SalesLT",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                computedColumnSql: "[City]+','+[PostalCode]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SearchTerm",
                schema: "SalesLT",
                table: "Address");
        }
    }
}
