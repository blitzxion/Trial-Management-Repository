using FluentResults;
using TrialManagement.Repository.Datasets;

namespace TrialManagement.Repository.Repository
{
    internal interface ITrialManagementRepository
    {
        Task<Result<Organization>> AddOrginization(Organization organization);
        Task<Result<ClinicalTrial>> AddClinicalTrail(ClinicalTrial clinicalTrial);
        Task<Result<ClinicalSite>> AddClinicalSite(ClinicalSite clinicalSite);
        Task<Result<Patient>> AddClinicalPatient(Patient Patient);
        Task<Result<PatientDataFile>> AddClinicalPatientDataFile(PatientDataFile patientDataFile);
        Task<Result<PatientSiteHistory>> AddClinicalPatientSiteHistoryFile(PatientSiteHistory patientSiteHistory);

        Result<IEnumerable<Organization>> FindOrganization(string orgName);
        Task<Result<Organization?>> GetOrganization(Guid orgId);
    }
}
