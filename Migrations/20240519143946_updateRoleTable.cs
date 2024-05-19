using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace petshop.Migrations
{
    /// <inheritdoc />
    public partial class updateRoleTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "create_at",
                table: "Roles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "update_at",
                table: "Roles",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "create_at",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "update_at",
                table: "Roles");
        }
    }
}
