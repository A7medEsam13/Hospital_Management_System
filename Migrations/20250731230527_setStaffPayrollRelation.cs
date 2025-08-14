using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setStaffPayrollRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Staffs_Payrolls_PayrollId",
                table: "Staffs");

            migrationBuilder.DropIndex(
                name: "IX_Staffs_PayrollId",
                table: "Staffs");

            migrationBuilder.DropColumn(
                name: "PayrollId",
                table: "Staffs");

            migrationBuilder.AlterColumn<string>(
                name: "StaffSSN",
                table: "Payrolls",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Payrolls_Staffs_StaffSSN",
                table: "Payrolls",
                column: "StaffSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payrolls_Staffs_StaffSSN",
                table: "Payrolls");

            migrationBuilder.DropIndex(
                name: "IX_Payrolls_StaffSSN",
                table: "Payrolls");

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

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_PayrollId",
                table: "Staffs",
                column: "PayrollId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Staffs_Payrolls_PayrollId",
                table: "Staffs",
                column: "PayrollId",
                principalTable: "Payrolls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
