using Microsoft.EntityFrameworkCore.Migrations;

namespace toyshop.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayItems_ProductItems_ProductItemId",
                table: "PayItems");

            migrationBuilder.RenameColumn(
                name: "ProductItemId",
                table: "PayItems",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_PayItems_ProductItemId",
                table: "PayItems",
                newName: "IX_PayItems_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayItems_Products_ProductId",
                table: "PayItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayItems_Products_ProductId",
                table: "PayItems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "PayItems",
                newName: "ProductItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PayItems_ProductId",
                table: "PayItems",
                newName: "IX_PayItems_ProductItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayItems_ProductItems_ProductItemId",
                table: "PayItems",
                column: "ProductItemId",
                principalTable: "ProductItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
