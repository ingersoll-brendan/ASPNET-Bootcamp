using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bootcamp.Data.Migrations
{
    /// <inheritdoc />
    public partial class BootcampMigrationUpdate9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AddressTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Billing" },
                    { 2, "Shipping" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AddressTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AddressTypes",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
