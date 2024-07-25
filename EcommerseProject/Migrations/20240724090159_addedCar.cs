using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerseProject.Migrations
{
    /// <inheritdoc />
    public partial class addedCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ShoppingCartItems");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_CarId",
                table: "ShoppingCartItems",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Cars_CarId",
                table: "ShoppingCartItems",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Cars_CarId",
                table: "ShoppingCartItems");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCartItems_CarId",
                table: "ShoppingCartItems");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
