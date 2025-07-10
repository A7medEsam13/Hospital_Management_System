using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class createBillsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Insurance_InsurancePolicyNumber",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Patients_PatientId",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Bill_BillId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Bill_BillId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Bill_BillId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bill",
                table: "Bill");

            migrationBuilder.RenameTable(
                name: "Bill",
                newName: "Bills");

            migrationBuilder.RenameIndex(
                name: "IX_Bill_PatientId",
                table: "Bills",
                newName: "IX_Bills_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Bill_InsurancePolicyNumber",
                table: "Bills",
                newName: "IX_Bills_InsurancePolicyNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bills",
                table: "Bills",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurance_InsurancePolicyNumber",
                table: "Bills",
                column: "InsurancePolicyNumber",
                principalTable: "Insurance",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Bills_BillId",
                table: "LaboratoryScreenings",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Bills_BillId",
                table: "Room",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurance_InsurancePolicyNumber",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Patients_PatientId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Bills_BillId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Bills_BillId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bills",
                table: "Bills");

            migrationBuilder.RenameTable(
                name: "Bills",
                newName: "Bill");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_PatientId",
                table: "Bill",
                newName: "IX_Bill_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Bills_InsurancePolicyNumber",
                table: "Bill",
                newName: "IX_Bill_InsurancePolicyNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bill",
                table: "Bill",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Insurance_InsurancePolicyNumber",
                table: "Bill",
                column: "InsurancePolicyNumber",
                principalTable: "Insurance",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Patients_PatientId",
                table: "Bill",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Bill_BillId",
                table: "LaboratoryScreenings",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Bill_BillId",
                table: "Medicines",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Bill_BillId",
                table: "Room",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
