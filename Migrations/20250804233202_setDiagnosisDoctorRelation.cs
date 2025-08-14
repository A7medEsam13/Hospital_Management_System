using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setDiagnosisDoctorRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoctorSSN",
                table: "Diagnoses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_DoctorSSN",
                table: "Diagnoses",
                column: "DoctorSSN");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnoses_Doctors_DoctorSSN",
                table: "Diagnoses",
                column: "DoctorSSN",
                principalTable: "Doctors",
                principalColumn: "SSN",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnoses_Doctors_DoctorSSN",
                table: "Diagnoses");

            migrationBuilder.DropIndex(
                name: "IX_Diagnoses_DoctorSSN",
                table: "Diagnoses");

            migrationBuilder.DropColumn(
                name: "DoctorSSN",
                table: "Diagnoses");
        }
    }
}
