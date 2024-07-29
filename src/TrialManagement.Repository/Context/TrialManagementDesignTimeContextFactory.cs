using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace TrialManagement.Repository.Context
{
    internal class TrialManagementDesignTimeContextFactory : IDesignTimeDbContextFactory<TrialManagementContext>
    {
        public TrialManagementContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TrialManagementContext>();
            builder.UseSqlServer();
            return new TrialManagementContext(builder.Options);
        }
    }
}
