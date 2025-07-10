using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setSSNKeyToStaff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Staff_ManagerId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Staff_StaffId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staff_StaffId",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staff_StaffId",
                table: "Payrolls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_StaffId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Staff");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "SSN",
                table: "Staff",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Payrolls",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "StaffId",
                table: "Nurses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "TechnicianId",
                table: "LaboratoryScreenings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "StaffSSN",
                table: "Doctors",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ManagerSSN",
                table: "Departments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "SSN");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_StaffSSN",
                table: "Doctors",
                column: "StaffSSN");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerSSN",
                table: "Departments",
                column: "ManagerSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Staff_ManagerSSN",
                table: "Departments",
                column: "ManagerSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Staff_StaffSSN",
                table: "Doctors",
                column: "StaffSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                table: "LaboratoryScreenings",
                column: "TechnicianId",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staff_StaffId",
                table: "Nurses",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staff_StaffId",
                table: "Payrolls",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Staff_ManagerSSN",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Staff_StaffSSN",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staff_StaffId",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staff_StaffId",
                table: "Payrolls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_StaffSSN",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_Departments_ManagerSSN",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "StaffSSN",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ManagerSSN",
                table: "Departments");

            migrationBuilder.AlterColumn<int>(
                name: "SSN",
                table: "Staff",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "Payrolls",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "StaffId",
                table: "Nurses",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "TechnicianId",
                table: "LaboratoryScreenings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "StaffId",
                table: "Doctors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_StaffId",
                table: "Doctors",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_ManagerId",
                table: "Departments",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Staff_ManagerId",
                table: "Departments",
                column: "ManagerId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Staff_StaffId",
                table: "Doctors",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                table: "LaboratoryScreenings",
                column: "TechnicianId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staff_StaffId",
                table: "Nurses",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staff_StaffId",
                table: "Payrolls",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
