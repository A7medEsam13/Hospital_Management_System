using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class setPrescriptionScreeningRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "LaboratoryScreenings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Report",
                table: "LaboratoryScreenings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "LaboratoryScreeningPrescription",
                columns: table => new
                {
                    PrescriptionId = table.Column<int>(type: "int", nullable: false),
                    LaboratoryScreeningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryScreeningPrescription", x => new { x.PrescriptionId, x.LaboratoryScreeningId });
                    table.ForeignKey(
                        name: "FK_LaboratoryScreeningPrescription_LaboratoryScreenings_LaboratoryScreeningId",
                        column: x => x.LaboratoryScreeningId,
                        principalTable: "LaboratoryScreenings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LaboratoryScreeningPrescription_Prescriptions_PrescriptionId",
                        column: x => x.PrescriptionId,
                        principalTable: "Prescriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryScreeningPrescription_LaboratoryScreeningId",
                table: "LaboratoryScreeningPrescription",
                column: "LaboratoryScreeningId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaboratoryScreeningPrescription");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "LaboratoryScreenings");

            migrationBuilder.DropColumn(
                name: "Report",
                table: "LaboratoryScreenings");
        }
    }
}
