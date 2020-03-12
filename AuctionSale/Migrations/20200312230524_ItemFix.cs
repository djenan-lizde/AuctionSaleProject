using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionSale.Migrations
{
    public partial class ItemFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitsInStock",
                table: "Item");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Item",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "UnitsInStock",
                table: "Item",
                nullable: false,
                defaultValue: 0);
        }
    }
}
