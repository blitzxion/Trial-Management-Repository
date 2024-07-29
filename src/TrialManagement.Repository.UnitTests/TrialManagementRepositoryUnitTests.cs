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
            var result = await sut.AddOrginization(organization);

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
            var result = await sut.AddOrginization(organization);

            // assert //
            result.IsFailed.Should().BeTrue();
            // TODO: check log message and generic error message //
        }

    }
}