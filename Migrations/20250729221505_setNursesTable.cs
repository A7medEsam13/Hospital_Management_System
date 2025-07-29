using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setNursesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staffs_StaffSSN",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Patients_PatientId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_PatientId",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses");

            migrationBuilder.DropIndex(
                name: "IX_Nurses_StaffSSN",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "DepartmentName",
                table: "Nurses");

            migrationBuilder.RenameColumn(
                name: "StaffSSN",
                table: "Nurses",
                newName: "SSN");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses",
                column: "SSN");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_RoomId",
                table: "Patients",
                column: "RoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staffs_SSN",
                table: "Nurses",
                column: "SSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Rooms_RoomId",
                table: "Patients",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nurses_Staffs_SSN",
                table: "Nurses");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Rooms_RoomId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_RoomId",
                table: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "SSN",
                table: "Nurses",
                newName: "StaffSSN");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Nurses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DepartmentName",
                table: "Nurses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nurses",
                table: "Nurses",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_PatientId",
                table: "Rooms",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_StaffSSN",
                table: "Nurses",
                column: "StaffSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Nurses_Staffs_StaffSSN",
                table: "Nurses",
                column: "StaffSSN",
                principalTable: "Staffs",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Patients_PatientId",
                table: "Rooms",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
