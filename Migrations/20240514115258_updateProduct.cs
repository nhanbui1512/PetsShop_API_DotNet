using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petshop.Migrations
{
    /// <inheritdoc />
    public partial class updateProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdateAt",
                table: "Products",
                newName: "update_at");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "product_name");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Products",
                newName: "create_at");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "update_at",
                table: "Products",
                newName: "UpdateAt");

            migrationBuilder.RenameColumn(
                name: "product_name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "create_at",
                table: "Products",
                newName: "CreateAt");
        }
    }
}
