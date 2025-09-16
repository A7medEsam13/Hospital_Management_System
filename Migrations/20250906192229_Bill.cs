using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class Bill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Bills_BillId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_PrescriptionMedicines_Bills_BillID",
                table: "PrescriptionMedicines");

            migrationBuilder.DropIndex(
                name: "IX_PrescriptionMedicines_BillID",
                table: "PrescriptionMedicines");

            migrationBuilder.DropIndex(
                name: "IX_LaboratoryScreenings_BillId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "PrescriptionMedicines");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "LaboratoryScreenings");

            migrationBuilder.AddColumn<int>(
                name: "BillID",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Prescriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_BillID",
                table: "Prescriptions",
                column: "BillID");

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Bills_BillID",
                table: "Prescriptions",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Bills_BillID",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_BillID",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "BillID",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Prescriptions");

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

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "LaboratoryScreenings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "LaboratoryScreenings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicines_BillID",
                table: "PrescriptionMedicines",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryScreenings_BillId",
                table: "LaboratoryScreenings",
                column: "BillId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Bills_BillId",
                table: "LaboratoryScreenings",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrescriptionMedicines_Bills_BillID",
                table: "PrescriptionMedicines",
                column: "BillID",
                principalTable: "Bills",
                principalColumn: "Id");
        }
    }
}
