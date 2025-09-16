using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_Insurances_PolicyNumber",
                table: "Bills");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropIndex(
                name: "IX_Bills_PolicyNumber",
                table: "Bills");

            migrationBuilder.DropColumn(
                name: "PolicyNumber",
                table: "Bills");

            migrationBuilder.AddColumn<bool>(
                name: "IsFullyPaid",
                table: "Bills",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFullyPaid",
                table: "Bills");

            migrationBuilder.AddColumn<int>(
                name: "PolicyNumber",
                table: "Bills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    PolicyNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    CoPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Coverage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dental = table.Column<bool>(type: "bit", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    InsuranceCode = table.Column<int>(type: "int", nullable: false),
                    Optical = table.Column<bool>(type: "bit", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.PolicyNumber);
                    table.ForeignKey(
                        name: "FK_Insurances_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bills_PolicyNumber",
                table: "Bills",
                column: "PolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_PatientId",
                table: "Insurances",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_Insurances_PolicyNumber",
                table: "Bills",
                column: "PolicyNumber",
                principalTable: "Insurances",
                principalColumn: "PolicyNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
