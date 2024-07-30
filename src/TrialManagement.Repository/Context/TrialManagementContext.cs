using TrialManagement.Repository.Datasets;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Patient>()
                .HasOne(e => e.CurrentClinicalSite)
                .WithMany(e=>e.Patients)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<PatientSiteHistory>()
                .HasOne(e => e.Patient)
                .WithMany(e => e.PatientSiteHistories)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
