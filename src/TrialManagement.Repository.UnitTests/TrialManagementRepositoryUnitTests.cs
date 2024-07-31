using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Moq;
using TrialManagement.Repository.Context;
using TrialManagement.Repository.Datasets;
using TrialManagement.Repository.Repository;

namespace TrialManagemnt.Repository.UnitTests
{
    public class TrialManagementRepositoryUnitTests
    {
        private readonly Mock<ILogger<TrialManagementRepository>> _logger;
        public TrialManagementRepositoryUnitTests() 
        {
            _logger = new Mock<ILogger<TrialManagementRepository>>();
        }

        private DbContextOptionsBuilder<TrialManagementContext> GetDbContextOptions() 
        {
            var optionsBuilder = new DbContextOptionsBuilder<TrialManagementContext>();
            optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            optionsBuilder.ConfigureWarnings(warnings =>
            {
                //InMemory Provider does not support transactions
                warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning);
            });

            return optionsBuilder;
        }
        
        #region constructors
        [Fact(DisplayName ="Constructor Should Throw Error If Null Context")]
        public void Constructor_ShouldThrowError_IfNullContext()
        {
            Action action = () => new TrialManagementRepository(null, _logger.Object);
            action.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("trialManagementContext");
        }

        [Fact(DisplayName = "Constructor Should Throw Error If Null Logger")]
        public void Constructor_ShouldThrowError_IfNullLogger()
        {
            // arrange //
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            
            Action action = () => new TrialManagementRepository(context, null);
            action.Should().Throw<ArgumentNullException>().Which.ParamName.Should().Be("logger");
        }
        #endregion

        #region orginzations
        [Theory(DisplayName = "GetOrganization Should Return Success If Orginzation Is Found"),AutoData]
        public async Task GetOrganization_ShouldReturnSuccess_IfOrginzationIsFound(Guid orgId)
        {
            // arrange //
            var organization = new Organization()
            {
                Id = orgId,
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            await context.Organizations.AddAsync(organization);
            await context.SaveChangesAsync();
            var sut = new TrialManagementRepository(context, _logger.Object);
            
            // act //
            var result = await sut.GetOrganization(organization.Id);

            // assert //
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(organization.Id);
        }

        [Fact(DisplayName = "AddOrganization Should Return Success")]
        public async Task AddOrganization_ShouldReturnSuccess()
        {
            // arrange //
            var organization = new Organization()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddOrganization(organization);

            // assert //
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(organization.Id);
        }
        
        [Fact(DisplayName = "AddOrganization Should Fail When No Name Provided")]
        public async Task AddOrganization_ShouldFail_WhenNoNameProvided()
        {
            // arrange //
            var organization = new Organization()
            {
                Description = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddOrganization(organization);

            // assert //
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().BeEquivalentTo(TrialManagementRepository.ErrorMessages.GenericErrorMessage);
        }
        #endregion

        #region trials
        [Fact(DisplayName = "AddClinicalTrail Should Return Success")]
        public async Task AddClinicalTrail_ShouldReturnSuccess()
        {
            // arrange //
            var dbObject = new ClinicalTrial()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddClinicalTrail(dbObject);

            // assert //
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(dbObject.Id);
        }

        [Fact(DisplayName = "AddClinicalTrail Should Fail When No Name Provided")]
        public async Task AddClinicalTrail_ShouldFail_WhenNoNameProvided()
        {
            // arrange //
            var dbObject = new ClinicalTrial()
            {
                Description = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddClinicalTrail(dbObject);

            // assert //
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().BeEquivalentTo(TrialManagementRepository.ErrorMessages.GenericErrorMessage);
        }

        [Fact(DisplayName = "AddClinicalTrail Should Succeed When Added To An Orginzation")]
        public async Task AddClinicalTrail_ShouldSucceed_WhenAddedToAnOrginzation()
        {
            // arrange //
            var orgObject = new Organization()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
            };
            var ctObject = new ClinicalTrial()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);
            var orgResult = await sut.AddOrganization(orgObject);
            ctObject.OrganizationId = orgResult.Value.Id;
            var ctResult = await sut.AddClinicalTrail(ctObject);

            // act //
            var queryOrgResult = await sut.GetOrganization(orgResult.Value.Id, true);

            // assert //
            queryOrgResult.IsSuccess.Should().BeTrue();
            queryOrgResult.Value.Should().NotBeNull();
            queryOrgResult.Value.ClinicalTrials.Should().HaveCount(1);
        }

        #endregion

        #region sites
        [Fact(DisplayName = "AddClinicalPatient Should Return Success")]
        public async Task AddClinicalSite_ShouldReturnSuccess()
        {
            // arrange //
            var dbObject = new ClinicalSite()
            {
                Name = Guid.NewGuid().ToString(),
                SitePrefix = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddClinicalSite(dbObject);

            // assert //
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(dbObject.Id);
        }

        [Fact(DisplayName = "AddClinicalSite Should Fail When No Name Provided")]
        public async Task AddClinicalSite_ShouldFail_WhenNoNameProvided()
        {
            // arrange //
            var dbObject = new ClinicalSite()
            {
                SitePrefix = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddClinicalSite(dbObject);

            // assert //
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().BeEquivalentTo(TrialManagementRepository.ErrorMessages.GenericErrorMessage);
        }

        [Fact(DisplayName = "AddClinicalSite Should Succeed When Added To A Trail")]
        public async Task AddClinicalSite_ShouldSucceed_WhenAddedToAnTrial()
        {
            // arrange //
            var ctObject = new ClinicalTrial()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
            var csObject = new ClinicalSite()
            {
                Name = Guid.NewGuid().ToString(),
                SitePrefix = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);
            var ctResult = await sut.AddClinicalTrail(ctObject);
            csObject.ClinicalTrialId = ctResult.Value.Id;
            var result = await sut.AddClinicalSite(csObject);
            // act //
            var queryCtResult = await sut.GetClinicalTrail(ctResult.Value.Id, true);

            // assert //
            queryCtResult.IsSuccess.Should().BeTrue();
            queryCtResult.Value.Should().NotBeNull();
            queryCtResult.Value.ClinicalSites.Should().HaveCount(1);
        }
        #endregion

        #region patients
        [Fact(DisplayName = "AddClinicalPatient Should Return Success")]
        public async Task AddClinicalPatient_ShouldReturnSuccess()
        {
            // arrange //
            var dbObject = new ClinicalPatient()
            {
                Identifier = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddClinicalPatient(dbObject);

            // assert //
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Id.Should().Be(dbObject.Id);
        }

        [Fact(DisplayName = "AddClinicalPatient Should Fail When No Name Provided")]
        public async Task AddClinicalPatient_ShouldFail_WhenNoNameProvided()
        {
            // arrange //
            var dbObject = new ClinicalPatient();
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);

            // act //
            var result = await sut.AddClinicalPatient(dbObject);

            // assert //
            result.IsFailed.Should().BeTrue();
            result.Errors[0].Message.Should().BeEquivalentTo(TrialManagementRepository.ErrorMessages.GenericErrorMessage);
        }

        [Fact(DisplayName = "AddClinicalPatient Should Succeed When Added To A Trail")]
        public async Task AddClinicalPatient_ShouldSucceed_WhenAddedToAnTrial()
        {
            // arrange //
            var ctObject = new ClinicalTrial()
            {
                Name = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString()
            };
            var cpObject = new ClinicalPatient()
            {
                Identifier = Guid.NewGuid().ToString(),
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);
            var ctResult = await sut.AddClinicalTrail(ctObject);
            cpObject.ClinicalTrialId = ctResult.Value.Id;
            var result = await sut.AddClinicalPatient(cpObject);
            // act //
            var queryCtResult = await sut.GetClinicalTrail(ctResult.Value.Id, false, true);

            // assert //
            queryCtResult.IsSuccess.Should().BeTrue();
            queryCtResult.Value.Should().NotBeNull();
            queryCtResult.Value.Patients.Should().HaveCount(1);
        }

        [Fact(DisplayName = "AddClinicalPatient Should Succeed When Added To A Site")]
        public async Task AddClinicalPatient_ShouldSucceed_WhenAddedToASite()
        {
            // arrange //
            var ctObject = new ClinicalSite()
            {
                Name = Guid.NewGuid().ToString(),
                SitePrefix = Guid.NewGuid().ToString(),
            };
            var cpObject = new ClinicalPatient()
            {
                Identifier = Guid.NewGuid().ToString()
            };
            var dbOptions = GetDbContextOptions();
            var context = new TrialManagementContext(dbOptions.Options);
            var sut = new TrialManagementRepository(context, _logger.Object);
            var csResult = await sut.AddClinicalSite(ctObject);
            cpObject.CurrentClinicalSiteId = csResult.Value.Id;
            var result = await sut.AddClinicalPatient(cpObject);
            // act //
            var queryCsResult = await sut.GetClinicalSite(csResult.Value.Id, true);

            // assert //
            queryCsResult.IsSuccess.Should().BeTrue();
            queryCsResult.Value.Should().NotBeNull();
            queryCsResult.Value.Patients.Should().HaveCount(1);
        }
        #endregion


    }
}