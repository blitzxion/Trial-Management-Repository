using TrialManagement.Repository.Datasets;
using Microsoft.EntityFrameworkCore;

namespace TrialManagement.Repository.Context
{
    public class TrialManagementContext: DbContext
    {
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<ClinicalTrial> ClinicalTrials { get; set; }
        public DbSet<ClinicalSite> ClinicalSites { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientSiteHistory> PatientSiteHistories { get; set; }
        public DbSet<PatientDataFile> PatientDataFiles { get; set; }

        public TrialManagementContext() { }
        public TrialManagementContext(DbContextOptions options) : base(options) { }

    }
}
