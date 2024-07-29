using FluentResults;
using TrialManagement.Repository.Context;
using TrialManagement.Repository.Datasets;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrialManagement.Repository.Repository
{
    public class TrialManagementRepository : ITrialManagementRepository
    {
        private readonly ILogger<TrialManagementRepository> _logger;
        private readonly TrialManagementContext _context;
        public TrialManagementRepository(TrialManagementContext trialManagementContext, ILogger<TrialManagementRepository> logger) 
        {
            _context = trialManagementContext ?? throw new ArgumentNullException(nameof(trialManagementContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<Organization>> AddOrginization(Organization organization)
        {
            try
            {
                var result = await _context.Organizations.AddAsync(organization);
                await _context.SaveChangesAsync();
                return Result.Ok(organization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public async Task<Result<ClinicalTrial>> AddClinicalTrail(ClinicalTrial clinicalTrial)
        {
            try
            {
                var result = await _context.ClinicalTrials.AddAsync(clinicalTrial);
                await _context.SaveChangesAsync();
                return Result.Ok(clinicalTrial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public async Task<Result<ClinicalSite>> AddClinicalSite(ClinicalSite clinicalSite)
        {
            try
            {
                var result = await _context.ClinicalSites.AddAsync(clinicalSite);
                await _context.SaveChangesAsync();
                return Result.Ok(clinicalSite);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public async Task<Result<Patient>> AddClinicalPatient(Patient patient)
        {
            try
            {
                var result = await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                return Result.Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public async Task<Result<PatientDataFile>> AddClinicalPatientDataFile(PatientDataFile patientDataFile)
        {
            try
            {
                var result = await _context.PatientDataFiles.AddAsync(patientDataFile);
                await _context.SaveChangesAsync();
                return Result.Ok(patientDataFile);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public async Task<Result<PatientSiteHistory>> AddClinicalPatientSiteHistoryFile(PatientSiteHistory patientSiteHistory)
        {
            try
            {
                var result = await _context.PatientSiteHistories.AddAsync(patientSiteHistory);
                await _context.SaveChangesAsync();
                return Result.Ok(patientSiteHistory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public async Task<Result<Organization?>> GetOrganization(Guid orgId) 
        {
            try
            {
                var result = await _context.Organizations.FindAsync(orgId);   
                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        public Result<IEnumerable<Organization>> FindOrganization(string orgName)
        {
            try
            {
                var result = _context.Organizations
                    .AsNoTracking()
                    .Where(x=> x.Name.Contains(orgName) == true).AsEnumerable();

                return Result.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Result.Fail(ErrorMessages.GenericErrorMessage);
            }
        }

        internal class ErrorMessages
        {
            public static string PatientNotFound = "Patient Not Found";
            public static string GenericErrorMessage = "An Error occured while processing your request. Information has been logged.";
            public static string PatientWithInvalidData(long patientId) => $"PatientId {patientId} is missing Diagnosis";
        }

    }
}
