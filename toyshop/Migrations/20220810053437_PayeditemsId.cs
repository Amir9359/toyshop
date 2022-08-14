using Microsoft.EntityFrameworkCore.Migrations;

namespace toyshop.Migrations
{
    public partial class PayeditemsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PayedCartItems_PayedCartItemsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_PayedCartItemsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PayedCartItemsId",
                table: "Products");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "PayedCartItems",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "PayedCartItems");

            migrationBuilder.AddColumn<int>(
                name: "PayedCartItemsId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_PayedCartItemsId",
                table: "Products",
                column: "PayedCartItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PayedCartItems_PayedCartItemsId",
                table: "Products",
                column: "PayedCartItemsId",
                principalTable: "PayedCartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
