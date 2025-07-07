using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootcamp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BootcampMigrationUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressIds",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "OrderIds",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressIds",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "OrderIds",
                table: "Customers");
        }
    }
}
