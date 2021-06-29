using AioCore.Infrastructure.DbContexts;
using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AioCore.API.Factories
{
    public class AioDynamicContextFactory : IDesignTimeDbContextFactory<AioDynamicContext>
    {
        public AioDynamicContext CreateDbContext(string[] args)
        {
            var configuration = AioCoreConfigs.Configuration(args);
            var provider = configuration.GetValue("Provider", "PostgresSql");
            var assemblyName = "DynamicMigrations." + provider;
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            var optionsBuilder = new DbContextOptionsBuilder<AioDynamicContext>();
            if (provider == "MsSql")
            {
                optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly(assemblyName));
            }
            else
            {
                optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly(assemblyName));
            }
            return new AioDynamicContext(optionsBuilder.Options, null)
            {
                Schema = provider == "MsSql" ? "dbo" : "public"
            };
        }
    }
}