using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurances_InsurancePolicyNumber",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staffs_StaffSSN",
                table: "Payrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bills_BillId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_BillId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions");

            migrationBuilder.DropIndex(
                name: "IX_Payrolls_StaffSSN",
                table: "Payrolls");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Bills_InsurancePolicyNumber",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "MedicineId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "InsurancePolicyNumber",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "PayrollId",
                table: "Staffs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StaffSSN",
                table: "Payrolls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Bills",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateTable(
                name: "MedicinePrescription",
                columns: table => new
                {
                    MedicinesId = table.Column<int>(type: "int", nullable: false),
                    PrescriptionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePrescription", x => new { x.MedicinesId, x.PrescriptionsId });
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_Medicines_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinePrescription_Prescriptions_PrescriptionsId",
                        column: x => x.PrescriptionsId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_PayrollId",
                table: "Staffs",
                column: "PayrollId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PolicyNumber",
                table: "Bills",
                column: "PolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_RoomId",
                table: "Bills",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_BillMedicine_MedicineId",
                table: "BillMedicine",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePrescription_PrescriptionsId",
                table: "MedicinePrescription",
                column: "PrescriptionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurances_PolicyNumber",
                table: "Bills",
                column: "PolicyNumber",
                principalTable: "Insurances",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Rooms_RoomId",
                table: "Bills",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Payrolls_PayrollId",
                table: "Staffs",
                column: "PayrollId",
                principalTable: "Payrolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurances_PolicyNumber",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Rooms_RoomId",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Payrolls_PayrollId",
                table: "Staffs");

            migrationBuilder.DropTable(
                name: "BillMedicine");

            migrationBuilder.DropTable(
                name: "MedicinePrescription");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_PayrollId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PolicyNumber",
                table: "Bills");

            migrationBuilder.DropIndex(
                name: "IX_Bills_RoomId",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MedicineId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "StaffSSN",
                table: "Payrolls",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsurancePolicyNumber",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_BillId",
                table: "Rooms",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prescriptions_MedicineId",
                table: "Prescriptions",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_InsurancePolicyNumber",
                table: "Bills",
                column: "InsurancePolicyNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurances_InsurancePolicyNumber",
                table: "Bills",
                column: "InsurancePolicyNumber",
                principalTable: "Insurances",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Bills_BillId",
                table: "Medicines",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staffs_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Medicines_MedicineId",
                table: "Prescriptions",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bills_BillId",
                table: "Rooms",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
