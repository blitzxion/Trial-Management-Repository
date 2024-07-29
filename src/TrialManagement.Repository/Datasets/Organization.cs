using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrialManagement.Repository.Datasets
{
    [Table(TableNames.Organization, Schema = SqlSchemas.TrailSchemaName)]
    public class Organization
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ClinicalTrial> ClinicalTrials { get; set; }
    }
}
