using FluentResults;
using TrialManagement.Repository.Datasets;

namespace TrialManagement.Repository.Repository
{
    public interface ITrialManagementRepository
    {
        Task<Result<Organization>> AddOrganization(Organization organization);
        Task<Result<ClinicalTrial>> AddClinicalTrail(ClinicalTrial clinicalTrial);
        Task<Result<ClinicalSite>> AddClinicalSite(ClinicalSite clinicalSite);
        Task<Result<ClinicalPatient>> AddClinicalPatient(ClinicalPatient Patient);
        Task<Result<PatientDataFile>> AddClinicalPatientDataFile(PatientDataFile patientDataFile);
        Task<Result<PatientSiteHistory>> AddClinicalPatientSiteHistoryFile(PatientSiteHistory patientSiteHistory);

        Result<IEnumerable<Organization>> SearchOrganizationsByName(string orgName);
        Task<Result<Organization?>> GetOrganization(Guid orgId, bool includeTrials = true);
        Task<Result<ClinicalTrial?>> GetClinicalTrail(Guid trialId, bool includeSites = false, bool includePatients = false);
        Task<Result<ClinicalSite?>> GetClinicalSite(Guid siteId, bool includePatients = false);
    }
}
