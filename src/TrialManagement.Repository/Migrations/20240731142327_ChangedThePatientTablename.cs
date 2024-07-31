using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrialManagement.Repository.Migrations
{
    public partial class ChangedThePatientTablename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientDataFiles_Patients_PatientId",
                schema: "TrialManagement",
                table: "PatientDataFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_ClinicalSites_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_ClinicalTrials_ClinicalTrialId",
                schema: "TrialManagement",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSiteHistories_Patients_PatientId",
                schema: "TrialManagement",
                table: "PatientSiteHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                schema: "TrialManagement",
                table: "Patients");

            migrationBuilder.RenameTable(
                name: "Patients",
                schema: "TrialManagement",
                newName: "ClinicalPatients",
                newSchema: "TrialManagement");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "ClinicalPatients",
                newName: "IX_ClinicalPatients_CurrentClinicalSiteId");

            migrationBuilder.RenameIndex(
                name: "IX_Patients_ClinicalTrialId",
                schema: "TrialManagement",
                table: "ClinicalPatients",
                newName: "IX_ClinicalPatients_ClinicalTrialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClinicalPatients",
                schema: "TrialManagement",
                table: "ClinicalPatients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalPatients_ClinicalSites_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "ClinicalPatients",
                column: "CurrentClinicalSiteId",
                principalSchema: "TrialManagement",
                principalTable: "ClinicalSites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClinicalPatients_ClinicalTrials_ClinicalTrialId",
                schema: "TrialManagement",
                table: "ClinicalPatients",
                column: "ClinicalTrialId",
                principalSchema: "TrialManagement",
                principalTable: "ClinicalTrials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDataFiles_ClinicalPatients_PatientId",
                schema: "TrialManagement",
                table: "PatientDataFiles",
                column: "PatientId",
                principalSchema: "TrialManagement",
                principalTable: "ClinicalPatients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSiteHistories_ClinicalPatients_PatientId",
                schema: "TrialManagement",
                table: "PatientSiteHistories",
                column: "PatientId",
                principalSchema: "TrialManagement",
                principalTable: "ClinicalPatients",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalPatients_ClinicalSites_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "ClinicalPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_ClinicalPatients_ClinicalTrials_ClinicalTrialId",
                schema: "TrialManagement",
                table: "ClinicalPatients");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientDataFiles_ClinicalPatients_PatientId",
                schema: "TrialManagement",
                table: "PatientDataFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientSiteHistories_ClinicalPatients_PatientId",
                schema: "TrialManagement",
                table: "PatientSiteHistories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClinicalPatients",
                schema: "TrialManagement",
                table: "ClinicalPatients");

            migrationBuilder.RenameTable(
                name: "ClinicalPatients",
                schema: "TrialManagement",
                newName: "Patients",
                newSchema: "TrialManagement");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicalPatients_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "Patients",
                newName: "IX_Patients_CurrentClinicalSiteId");

            migrationBuilder.RenameIndex(
                name: "IX_ClinicalPatients_ClinicalTrialId",
                schema: "TrialManagement",
                table: "Patients",
                newName: "IX_Patients_ClinicalTrialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                schema: "TrialManagement",
                table: "Patients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientDataFiles_Patients_PatientId",
                schema: "TrialManagement",
                table: "PatientDataFiles",
                column: "PatientId",
                principalSchema: "TrialManagement",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_ClinicalSites_CurrentClinicalSiteId",
                schema: "TrialManagement",
                table: "Patients",
                column: "CurrentClinicalSiteId",
                principalSchema: "TrialManagement",
                principalTable: "ClinicalSites",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_ClinicalTrials_ClinicalTrialId",
                schema: "TrialManagement",
                table: "Patients",
                column: "ClinicalTrialId",
                principalSchema: "TrialManagement",
                principalTable: "ClinicalTrials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientSiteHistories_Patients_PatientId",
                schema: "TrialManagement",
                table: "PatientSiteHistories",
                column: "PatientId",
                principalSchema: "TrialManagement",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
