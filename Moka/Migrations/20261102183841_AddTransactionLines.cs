using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moka.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Machines_MachineId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Stores_DestinationId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Stores_OriginId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_MachineId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "Transaction");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_OriginId",
                table: "Transactions",
                newName: "IX_Transactions_OriginId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_DestinationId",
                table: "Transactions",
                newName: "IX_Transactions_DestinationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TransactionLines",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    LineId = table.Column<int>(type: "int", nullable: false),
                    MachineId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionLines", x => new { x.TransactionId, x.LineId });
                    table.ForeignKey(
                        name: "FK_TransactionLines_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransactionLines_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_StoreId",
                table: "Machines",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionLines_MachineId",
                table: "TransactionLines",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Machines_Stores_StoreId",
                table: "Machines",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Stores_DestinationId",
                table: "Transactions",
                column: "DestinationId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Stores_OriginId",
                table: "Transactions",
                column: "OriginId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Machines_Stores_StoreId",
                table: "Machines");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Stores_DestinationId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Stores_OriginId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "TransactionLines");

            migrationBuilder.DropIndex(
                name: "IX_Machines_StoreId",
                table: "Machines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OriginId",
                table: "Transaction",
                newName: "IX_Transaction_OriginId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_DestinationId",
                table: "Transaction",
                newName: "IX_Transaction_DestinationId");

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_MachineId",
                table: "Transaction",
                column: "MachineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Machines_MachineId",
                table: "Transaction",
                column: "MachineId",
                principalTable: "Machines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Stores_DestinationId",
                table: "Transaction",
                column: "DestinationId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Stores_OriginId",
                table: "Transaction",
                column: "OriginId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
