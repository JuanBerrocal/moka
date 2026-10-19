using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moka.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    OriginId = table.Column<int>(type: "int", nullable: false),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    MovementType = table.Column<int>(type: "int", nullable: false),
                    Process = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Temporary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Stores_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_Stores_OriginId",
                        column: x => x.OriginId,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_DestinationId",
                table: "Transaction",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MachineId",
                table: "Transaction",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_OriginId",
                table: "Transaction",
                column: "OriginId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
