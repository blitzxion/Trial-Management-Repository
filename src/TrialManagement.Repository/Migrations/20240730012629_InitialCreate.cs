using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrialManagement.Repository.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "TrialManagement");

            migrationBuilder.CreateTable(
                name: "Organizations",
                schema: "TrialManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalTrials",
                schema: "TrialManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalTrials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalTrials_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalSchema: "TrialManagement",
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClinicalSites",
                schema: "TrialManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SitePrefix = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClinicalTrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClinicalSites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClinicalSites_ClinicalTrials_ClinicalTrialId",
                        column: x => x.ClinicalTrialId,
                        principalSchema: "TrialManagement",
                        principalTable: "ClinicalTrials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "TrialManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identifier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentClinicalSiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicalTrialId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_ClinicalSites_CurrentClinicalSiteId",
                        column: x => x.CurrentClinicalSiteId,
                        principalSchema: "TrialManagement",
                        principalTable: "ClinicalSites",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patients_ClinicalTrials_ClinicalTrialId",
                        column: x => x.ClinicalTrialId,
                        principalSchema: "TrialManagement",
                        principalTable: "ClinicalTrials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientDataFiles",
                schema: "TrialManagement",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientDataFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientDataFiles_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "TrialManagement",
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientSiteHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EndDateTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClinicalSiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientSiteHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientSiteHistories_ClinicalSites_ClinicalSiteId",
                        column: x => x.ClinicalSiteId,
                        principalSchema: "TrialManagement",
                        principalTable: "ClinicalSites",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientSiteHistories_Patients_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "TrialManagement",
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalSites_ClinicalTrialId",
                schema: "TrialManagement",
                table: "ClinicalSites",
                column: "ClinicalTrialId");

            migrationBuilder.CreateIndex(
                name: "IX_ClinicalTrials_OrganizationId",
                schema: "TrialManagement",
                table: "ClinicalTrials",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientDataFiles_PatientId",
                schema: "TrialManagement",
                table: "PatientDataFiles",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ClinicalTrialId",
                schema: "TrialManagement",
                table: "Patients",
                column: "ClinicalTrialId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "Patients",
                column: "CurrentClinicalSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSiteHistories_ClinicalSiteId",
                table: "PatientSiteHistories",
                column: "ClinicalSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientSiteHistories_PatientId",
                table: "PatientSiteHistories",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientDataFiles",
                schema: "TrialManagement");

            migrationBuilder.DropTable(
                name: "PatientSiteHistories");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "TrialManagement");

            migrationBuilder.DropTable(
                name: "ClinicalSites",
                schema: "TrialManagement");

            migrationBuilder.DropTable(
                name: "ClinicalTrials",
                schema: "TrialManagement");

            migrationBuilder.DropTable(
                name: "Organizations",
                schema: "TrialManagement");
        }
    }
}
