# Trial-Management-Repository

Entity Framework database w/ Repository and Result pattern

## Datasets

- list of currently available datasets

```
DbSet<Organization> Organizations
DbSet<ClinicalTrial> ClinicalTrials
DbSet<ClinicalSite> ClinicalSites
DbSet<Patient> Patients
DbSet<PatientSiteHistory> PatientSiteHistories
DbSet<PatientDataFile> PatientDataFiles
```

### Setup

- When setting up the `ITrialManagementRepository` a `TrialManagementContext`
  and an `ILogger<TrialManagementRepository>` are required.

#### TrialManagementContext Setup

- The context will be setup with the builder options `DbContextOptionsBuilder`
  it can be setup with a sql or InMemory database
- Example for InMemory context setup:

```
var optionsBuilder = new DbContextOptionsBuilder<TrialManagementContext>();
optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
optionsBuilder.ConfigureWarnings(warnings =>
{
    //InMemory Provider does not support transactions
    warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning);
});

new TrialManagementContext(optionsBuilder.Options);
```

## ITrialManagementRepository Current Operations

- List of Current Interface

```
// add functionality //
Task<Result<Organization>> AddOrganization(Organization organization);
Task<Result<ClinicalTrial>> AddClinicalTrail(ClinicalTrial clinicalTrial);
Task<Result<ClinicalSite>> AddClinicalSite(ClinicalSite clinicalSite);
Task<Result<Patient>> AddClinicalPatient(Patient Patient);
Task<Result<PatientDataFile>> AddClinicalPatientDataFile(PatientDataFile patientDataFile);
Task<Result<PatientSiteHistory>> AddClinicalPatientSiteHistoryFile(PatientSiteHistory patientSiteHistory);

// Search functionality //
Result<IEnumerable<Organization>> FindOrganization(string orgName);
Task<Result<Organization?>> GetOrganization(Guid orgId);
```

## Outputs

- All outputs use the FluentResults to create to results
- All outputs have an **IsSuccess** and an **IsFailed** property that will allow
  a developer to determine if the query, insert, etc was successful
- All exceptions will be logged to the `ILogger<TrialManagementRepository>`
  parameter in the constructor
