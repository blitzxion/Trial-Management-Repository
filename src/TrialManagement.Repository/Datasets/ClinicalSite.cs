using Microsoft.EntityFrameworkCore;
using TrialManagement.Repository.Datasets;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrialManagement.Repository.Datasets
{
    [Table(TableNames.ClinicalSite, Schema = SqlSchemas.TrailSchemaName)]
    public class ClinicalSite
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string SitePrefix { get; set; }
        
        public Guid ClinicalTrialId { get; set; }
        [ForeignKey(nameof(ClinicalTrialId))]
        public ClinicalTrial ClinicalTrial { get; set; }
        
        public ICollection<Patient> Patients { get; set; }
    }
}
