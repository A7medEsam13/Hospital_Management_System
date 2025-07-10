using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class createLaboratoryScreeningsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BillId",
                table: "Medicines",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Insurance",
                columns: table => new
                {
                    PolicyNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    InsuranceCode = table.Column<int>(type: "int", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoPay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Coverage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optical = table.Column<bool>(type: "bit", nullable: false),
                    Dental = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurance", x => x.PolicyNumber);
                    table.ForeignKey(
                        name: "FK_Insurance_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    RoomCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TestCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherCharges = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedicineCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RemainingBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PolicyNumber = table.Column<int>(type: "int", nullable: false),
                    InsurancePolicyNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bill_Insurance_InsurancePolicyNumber",
                        column: x => x.InsurancePolicyNumber,
                        principalTable: "Insurance",
                        principalColumn: "PolicyNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bill_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LaboratoryScreenings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TechnicianId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    TestCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryScreenings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryScreenings_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryScreenings_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryScreenings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LaboratoryScreenings_Staff_TechnicianId",
                        column: x => x.TechnicianId,
                        principalTable: "Staff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Room_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Room_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_InsurancePolicyNumber",
                table: "Bill",
                column: "InsurancePolicyNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_PatientId",
                table: "Bill",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Insurance_PatientId",
                table: "Insurance",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryScreenings_BillId",
                table: "LaboratoryScreenings",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryScreenings_DoctorId",
                table: "LaboratoryScreenings",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryScreenings_PatientId",
                table: "LaboratoryScreenings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryScreenings_TechnicianId",
                table: "LaboratoryScreenings",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Room_BillId",
                table: "Room",
                column: "BillId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Room_PatientId",
                table: "Room",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Bill_BillId",
                table: "Medicines",
                column: "BillId",
                principalTable: "Bill",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Bill_BillId",
                table: "Medicines");

            migrationBuilder.DropTable(
                name: "LaboratoryScreenings");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Insurance");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_BillId",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "BillId",
                table: "Medicines");
        }
    }
}
