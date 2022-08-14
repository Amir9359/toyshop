using Microsoft.EntityFrameworkCore.Migrations;

namespace toyshop.Migrations
{
    public partial class PayeditemesMigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PayedCartItems_PayedCartid",
                table: "PayedCartItems");

            migrationBuilder.CreateIndex(
                name: "IX_PayedCartItems_PayedCartid",
                table: "PayedCartItems",
                column: "PayedCartid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PayedCartItems_PayedCartid",
                table: "PayedCartItems");

            migrationBuilder.CreateIndex(
                name: "IX_PayedCartItems_PayedCartid",
                table: "PayedCartItems",
                column: "PayedCartid",
                unique: true);
        }
    }
}
