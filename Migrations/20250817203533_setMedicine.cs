using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setMedicine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicine_Medicines_MedicineId",
                table: "PrescriptionMedicine");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicine_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicine");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicine",
                table: "PrescriptionMedicine");

            migrationBuilder.DropColumn(
                name: "DurationInDays",
                table: "PrescriptionMedicine");

            migrationBuilder.RenameTable(
                name: "PrescriptionMedicine",
                newName: "PrescriptionMedicines");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionMedicine_MedicineId",
                table: "PrescriptionMedicines",
                newName: "IX_PrescriptionMedicines_MedicineId");

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "PrescriptionMedicines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicines",
                table: "PrescriptionMedicines",
                columns: new[] { "PrescriptionId", "MedicineId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Medicines_MedicineId",
                table: "PrescriptionMedicines",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicines",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Medicines_MedicineId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicines");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrescriptionMedicines",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "PrescriptionMedicines");

            migrationBuilder.RenameTable(
                name: "PrescriptionMedicines",
                newName: "PrescriptionMedicine");

            migrationBuilder.RenameIndex(
                name: "IX_PrescriptionMedicines_MedicineId",
                table: "PrescriptionMedicine",
                newName: "IX_PrescriptionMedicine_MedicineId");

            migrationBuilder.AddColumn<int>(
                name: "DurationInDays",
                table: "PrescriptionMedicine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrescriptionMedicine",
                table: "PrescriptionMedicine",
                columns: new[] { "PrescriptionId", "MedicineId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicine_Medicines_MedicineId",
                table: "PrescriptionMedicine",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicine_Prescriptions_PrescriptionId",
                table: "PrescriptionMedicine",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
