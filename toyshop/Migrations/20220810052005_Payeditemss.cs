using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace toyshop.Migrations
{
    public partial class Payeditemss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PayedCartItemsId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PayedCarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FishSabt = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PayedCartItemID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayedCarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PayedCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PayedCartid = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayedCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PayedCartItems_PayedCarts_PayedCartid",
                        column: x => x.PayedCartid,
                        principalTable: "PayedCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_PayedCartItemsId",
                table: "Products",
                column: "PayedCartItemsId");

            migrationBuilder.CreateIndex(
                name: "IX_PayedCartItems_PayedCartid",
                table: "PayedCartItems",
                column: "PayedCartid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_PayedCartItems_PayedCartItemsId",
                table: "Products",
                column: "PayedCartItemsId",
                principalTable: "PayedCartItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_PayedCartItems_PayedCartItemsId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "PayedCartItems");

            migrationBuilder.DropTable(
                name: "PayedCarts");

            migrationBuilder.DropIndex(
                name: "IX_Products_PayedCartItemsId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PayedCartItemsId",
                table: "Products");
        }
    }
}
