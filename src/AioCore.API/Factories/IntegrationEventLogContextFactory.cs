using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Package.EventBus.IntegrationEventLogEF;
using System.Reflection;

namespace AioCore.API.Factories
{
    public class IntegrationEventLogContextFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>();
            optionsBuilder.UseNpgsql(b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }
}