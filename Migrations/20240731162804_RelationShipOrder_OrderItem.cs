using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petshop.Migrations
{
    /// <inheritdoc />
    public partial class RelationShipOrder_OrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Options_OptionId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems");

            migrationBuilder.AddColumn<int>(
                name: "OptionId1",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OptionId1",
                table: "OrderItems",
                column: "OptionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Options_OptionId1",
                table: "OrderItems",
                column: "OptionId1",
                principalTable: "Options",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OptionId",
                table: "OrderItems",
                column: "OptionId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Options_OptionId1",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OptionId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_OptionId1",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "OptionId1",
                table: "OrderItems");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Options_OptionId",
                table: "OrderItems",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
