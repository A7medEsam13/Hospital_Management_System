using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setDoctorsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Staffs_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Staffs_DoctorsSSN",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staffs_DoctorId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Staffs_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Staffs");

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    SSN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.SSN);
                    table.ForeignKey(
                        name: "FK_Doctors_Staffs_SSN",
                        column: x => x.SSN,
                        principalTable: "Staffs",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Doctors_DoctorsSSN",
                table: "DoctorPatient",
                column: "DoctorsSSN",
                principalTable: "Doctors",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Doctors_DoctorId",
                table: "LaboratoryScreenings",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Doctors_DoctorsSSN",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Doctors_DoctorId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Staffs",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Staffs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Staffs_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Staffs_DoctorsSSN",
                table: "DoctorPatient",
                column: "DoctorsSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staffs_DoctorId",
                table: "LaboratoryScreenings",
                column: "DoctorId",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Staffs_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
