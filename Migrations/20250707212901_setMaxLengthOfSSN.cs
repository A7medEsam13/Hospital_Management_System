using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setMaxLengthOfSSN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staff_StaffId",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staff_StaffId",
                table: "Payrolls");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Payrolls",
                newName: "StaffSSN");

            migrationBuilder.RenameIndex(
                name: "IX_Payrolls_StaffId",
                table: "Payrolls",
                newName: "IX_Payrolls_StaffSSN");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "Nurses",
                newName: "StaffSSN");

            migrationBuilder.RenameIndex(
                name: "IX_Nurses_StaffId",
                table: "Nurses",
                newName: "IX_Nurses_StaffSSN");

            migrationBuilder.RenameColumn(
                name: "TechnicianId",
                table: "LaboratoryScreenings",
                newName: "TechnicianSSN");

            migrationBuilder.RenameIndex(
                name: "IX_LaboratoryScreenings_TechnicianId",
                table: "LaboratoryScreenings",
                newName: "IX_LaboratoryScreenings_TechnicianSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianSSN",
                table: "LaboratoryScreenings",
                column: "TechnicianSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staff_StaffSSN",
                table: "Nurses",
                column: "StaffSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staff_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianSSN",
                table: "LaboratoryScreenings");

            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staff_StaffSSN",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staff_StaffSSN",
                table: "Payrolls");

            migrationBuilder.RenameColumn(
                name: "StaffSSN",
                table: "Payrolls",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Payrolls_StaffSSN",
                table: "Payrolls",
                newName: "IX_Payrolls_StaffId");

            migrationBuilder.RenameColumn(
                name: "StaffSSN",
                table: "Nurses",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_Nurses_StaffSSN",
                table: "Nurses",
                newName: "IX_Nurses_StaffId");

            migrationBuilder.RenameColumn(
                name: "TechnicianSSN",
                table: "LaboratoryScreenings",
                newName: "TechnicianId");

            migrationBuilder.RenameIndex(
                name: "IX_LaboratoryScreenings_TechnicianSSN",
                table: "LaboratoryScreenings",
                newName: "IX_LaboratoryScreenings_TechnicianId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                table: "LaboratoryScreenings",
                column: "TechnicianId",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staff_StaffId",
                table: "Nurses",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staff_StaffId",
                table: "Payrolls",
                column: "StaffId",
                principalTable: "Staff",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
