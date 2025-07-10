using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class createRoomsAndInsurancesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurance_InsurancePolicyNumber",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurance_Patients_PatientId",
                table: "Insurance");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Bills_BillId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_Room_Patients_PatientId",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insurance",
                table: "Insurance");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameTable(
                name: "Insurance",
                newName: "Insurances");

            migrationBuilder.RenameIndex(
                name: "IX_Room_PatientId",
                table: "Rooms",
                newName: "IX_Rooms_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_BillId",
                table: "Rooms",
                newName: "IX_Rooms_BillId");

            migrationBuilder.RenameIndex(
                name: "IX_Insurance_PatientId",
                table: "Insurances",
                newName: "IX_Insurances_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insurances",
                table: "Insurances",
                column: "PolicyNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurances_InsurancePolicyNumber",
                table: "Bills",
                column: "InsurancePolicyNumber",
                principalTable: "Insurances",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurances_Patients_PatientId",
                table: "Insurances",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Bills_BillId",
                table: "Rooms",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Patients_PatientId",
                table: "Rooms",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurances_InsurancePolicyNumber",
                table: "Bills");

            migrationBuilder.DropForeignKey(
                name: "FK_Insurances_Patients_PatientId",
                table: "Insurances");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Bills_BillId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Patients_PatientId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Insurances",
                table: "Insurances");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "Insurances",
                newName: "Insurance");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_PatientId",
                table: "Room",
                newName: "IX_Room_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_BillId",
                table: "Room",
                newName: "IX_Room_BillId");

            migrationBuilder.RenameIndex(
                name: "IX_Insurances_PatientId",
                table: "Insurance",
                newName: "IX_Insurance_PatientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Insurance",
                table: "Insurance",
                column: "PolicyNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurance_InsurancePolicyNumber",
                table: "Bills",
                column: "InsurancePolicyNumber",
                principalTable: "Insurance",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Insurance_Patients_PatientId",
                table: "Insurance",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Bills_BillId",
                table: "Room",
                column: "BillId",
                principalTable: "Bills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Patients_PatientId",
                table: "Room",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
