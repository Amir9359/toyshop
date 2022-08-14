using Microsoft.EntityFrameworkCore.Migrations;

namespace toyshop.Migrations
{
    public partial class PayeditemesCustomerMigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CusstomerId",
                table: "PayedCarts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PayedCarts_CusstomerId",
                table: "PayedCarts",
                column: "CusstomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PayedCarts_AspNetUsers_CusstomerId",
                table: "PayedCarts",
                column: "CusstomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PayedCarts_AspNetUsers_CusstomerId",
                table: "PayedCarts");

            migrationBuilder.DropIndex(
                name: "IX_PayedCarts_CusstomerId",
                table: "PayedCarts");

            migrationBuilder.DropColumn(
                name: "CusstomerId",
                table: "PayedCarts");
        }
    }
}
