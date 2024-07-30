using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrialManagement.Repository.Migrations
{
    public partial class MovePatientSiteHistory_ToSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PatientSiteHistories",
                newName: "PatientSiteHistories",
                newSchema: "TrialManagement");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "PatientSiteHistories",
                schema: "TrialManagement",
                newName: "PatientSiteHistories");
        }
    }
}
