using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class editBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillMedicine");

            migrationBuilder.DropColumn(
                name: "OtherCharges",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "BillID",
                table: "PrescriptionMedicines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "PrescriptionMedicines",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "LaboratoryScreenings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicines_BillID",
                table: "PrescriptionMedicines",
                column: "BillID");

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Bills_BillID",
                table: "PrescriptionMedicines",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Bills_BillID",
                table: "PrescriptionMedicines");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionMedicines_BillID",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "LaboratoryScreenings");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Bills");

            migrationBuilder.AddColumn<decimal>(
                name: "OtherCharges",
                table: "Bills",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "BillMedicine",
                columns: table => new
                {
                    BillsId = table.Column<int>(type: "int", nullable: false),
                    MedicineId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillMedicine", x => new { x.BillsId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_BillMedicine_Bills_BillsId",
                        column: x => x.BillsId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillMedicine_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillMedicine_MedicineId",
                table: "BillMedicine",
                column: "MedicineId");
        }
    }
}
