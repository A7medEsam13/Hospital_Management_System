using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class SetDoctorRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorPatient_Doctors_DoctorsId",
                table: "DoctorPatient");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Doctors_DoctorId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorPatient",
                table: "DoctorPatient");

            migrationBuilder.DropColumn(
                name: "DoctorsId",
                table: "DoctorPatient");

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

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Prescriptions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "LaboratoryScreenings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "DoctorsSSN",
                table: "DoctorPatient",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "DoctorId",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorPatient",
                table: "DoctorPatient",
                columns: new[] { "DoctorsSSN", "PatientsId" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorPatient",
                table: "DoctorPatient");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "DoctorsSSN",
                table: "DoctorPatient");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "LaboratoryScreenings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "DoctorsId",
                table: "DoctorPatient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DoctorId",
                table: "Appointments",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorPatient",
                table: "DoctorPatient",
                columns: new[] { "DoctorsId", "PatientsId" });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffSSN = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Staffs_StaffSSN",
                        column: x => x.StaffSSN,
                        principalTable: "Staffs",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_StaffSSN",
                table: "Doctors",
                column: "StaffSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Doctors_DoctorId",
                table: "Appointments",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorPatient_Doctors_DoctorsId",
                table: "DoctorPatient",
                column: "DoctorsId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Doctors_DoctorId",
                table: "LaboratoryScreenings",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Prescriptions_Doctors_DoctorId",
                table: "Prescriptions",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
