using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moka.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUniqueSapCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_SapCode",
                table: "Stores");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SapCode",
                table: "Stores",
                column: "SapCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Stores_SapCode",
                table: "Stores");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_SapCode",
                table: "Stores",
                column: "SapCode",
                unique: true,
                filter: "[SapCode] IS NOT NULL");
        }
    }
}
