using System.ComponentModel.DataAnnotations.Schema;

namespace TrialManagement.Repository.Datasets
{
    [Table(TableNames.PatientSiteHistory, Schema = SqlSchemas.TrailSchemaName)]
    public class PatientSiteHistory
    {
        public Guid Id { get; set; }
        
        public DateTimeOffset StartDateTime { get; set; }
        public DateTimeOffset? EndDateTime { get; set; }

        public Guid PatientId { get; set; }
        [ForeignKey(nameof(PatientId))]
        public ClinicalPatient Patient { get; set; }
        
        public Guid ClinicalSiteId { get; set; }
        [ForeignKey(nameof(ClinicalSiteId))]
        public ClinicalSite ClinicalSite { get; set; }

    }
}
