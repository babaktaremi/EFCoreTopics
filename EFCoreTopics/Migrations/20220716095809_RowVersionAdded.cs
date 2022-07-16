using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFCoreTopics.Migrations
{
    public partial class RowVersionAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "SharedWallets");

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "SharedWallets",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "SharedWallets");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "SharedWallets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
