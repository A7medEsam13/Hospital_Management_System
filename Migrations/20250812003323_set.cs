using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class set : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosesId",
                table: "DiagnosisPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisPatient_Patients_PatientsId",
                table: "DiagnosisPatient");

            migrationBuilder.DropIndex(
                name: "IX_Patients_RoomId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Rooms");

            
            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosisId",
                table: "DiagnosisPatient",
                column: "DiagnosisId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisPatient_Patients_PatientId",
                table: "DiagnosisPatient",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosisId",
                table: "DiagnosisPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_DiagnosisPatient_Patients_PatientId",
                table: "DiagnosisPatient");

            migrationBuilder.DropIndex(
                name: "IX_Patients_RoomId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "DiagnosisId",
                table: "DiagnosisPatient",
                newName: "PatientsId");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "DiagnosisPatient",
                newName: "DiagnosesId");

            migrationBuilder.RenameIndex(
                name: "IX_DiagnosisPatient_DiagnosisId",
                table: "DiagnosisPatient",
                newName: "IX_DiagnosisPatient_PatientsId");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId",
                unique: true,
                filter: "[RoomId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisPatient_Diagnoses_DiagnosesId",
                table: "DiagnosisPatient",
                column: "DiagnosesId",
                principalTable: "Diagnoses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DiagnosisPatient_Patients_PatientsId",
                table: "DiagnosisPatient",
                column: "PatientsId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
