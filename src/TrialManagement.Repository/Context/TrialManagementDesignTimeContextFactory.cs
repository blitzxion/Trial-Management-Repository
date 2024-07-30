using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace TrialManagement.Repository.Context
{
    internal class TrialManagementDesignTimeContextFactory : IDesignTimeDbContextFactory<TrialManagementContext>
    {
        public TrialManagementContext CreateDbContext(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = configBuilder.Build();
            var builder = new DbContextOptionsBuilder<TrialManagementContext>();
            builder.UseSqlServer(connectionString: config.GetConnectionString("DesignTimeConnection"));
            return new TrialManagementContext(builder.Options);
        }
    }
}
