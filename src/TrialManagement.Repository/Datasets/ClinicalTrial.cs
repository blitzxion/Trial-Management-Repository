using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrialManagement.Repository.Datasets
{
    [Table(TableNames.ClinicalTrial, Schema = SqlSchemas.TrailSchemaName)]
    public class ClinicalTrial
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public bool IsActive { get; set; }
        
        public Guid OrganizationId { get; set; }
        [ForeignKey(nameof(OrganizationId))]
        public Organization Organization { get; set; }
        
        public ICollection<Patient> Patients { get; set; }
        
        public ICollection<ClinicalSite> ClinicalSites { get; set; }
    }
}
