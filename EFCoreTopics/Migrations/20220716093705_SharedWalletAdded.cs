using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTopics.Migrations
{
    public partial class SharedWalletAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedWallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    WalletName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WalletAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedWallets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedWallets");
        }
    }
}
