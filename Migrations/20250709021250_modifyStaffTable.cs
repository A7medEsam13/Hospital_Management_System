using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class modifyStaffTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Staff_ManagerSSN",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Staff_StaffSSN",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianSSN",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staff_StaffSSN",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staff_StaffSSN",
                table: "Payrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Staff_Departments_DepartmentId",
                table: "Staff");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staff",
                table: "Staff");

            migrationBuilder.RenameTable(
                name: "Staff",
                newName: "Staffs");

            migrationBuilder.RenameIndex(
                name: "IX_Staff_DepartmentId",
                table: "Staffs",
                newName: "IX_Staffs_DepartmentId");

            migrationBuilder.AddColumn<bool>(
                name: "IsTerminated",
                table: "Staffs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "SSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Staffs_ManagerSSN",
                table: "Departments",
                column: "ManagerSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Staffs_StaffSSN",
                table: "Doctors",
                column: "StaffSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staffs_TechnicianSSN",
                table: "LaboratoryScreenings",
                column: "TechnicianSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staffs_StaffSSN",
                table: "Nurses",
                column: "StaffSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staffs_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Departments_DepartmentId",
                table: "Staffs",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Staffs_ManagerSSN",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Staffs_StaffSSN",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staffs_TechnicianSSN",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staffs_StaffSSN",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staffs_StaffSSN",
                table: "Payrolls");

            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Departments_DepartmentId",
                table: "Staffs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "IsTerminated",
                table: "Staffs");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "Staff");

            migrationBuilder.RenameIndex(
                name: "IX_Staffs_DepartmentId",
                table: "Staff",
                newName: "IX_Staff_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staff",
                table: "Staff",
                column: "SSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Staff_ManagerSSN",
                table: "Departments",
                column: "ManagerSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Staff_StaffSSN",
                table: "Doctors",
                column: "StaffSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianSSN",
                table: "LaboratoryScreenings",
                column: "TechnicianSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staff_StaffSSN",
                table: "Nurses",
                column: "StaffSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staff_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Staff_Departments_DepartmentId",
                table: "Staff",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
