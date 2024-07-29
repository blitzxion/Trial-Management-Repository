using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrialManagement.Repository.Datasets
{
    [Table(TableNames.Patient, Schema = SqlSchemas.TrailSchemaName)]
    public class Patient
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Identifier is required")]
        public string Identifier { get; set; }
        
        public Guid CurrentClinicalSiteId { get; set; }
        [ForeignKey(nameof(CurrentClinicalSiteId))]
        public ClinicalSite CurrentClinicalSite { get; set; }
        
        public Guid ClinicalTrialId { get; set; }
        [ForeignKey(nameof(ClinicalTrialId))]
        public ClinicalTrial ClinicalTrial { get; set; }
        
        public ICollection<PatientDataFile> PatientDataFiles { get; set; }

        public ICollection<PatientSiteHistory> PatientSiteHistories { get; set; }
    }
}
