using Microsoft.EntityFrameworkCore.Migrations;

namespace AuctionSale.Migrations
{
    public partial class BidsItemChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BidsItems",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BidsItems",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
