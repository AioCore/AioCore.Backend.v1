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
            var assembly = "DynamicMigrations." + provider;
            var optionsBuilder = new DbContextOptionsBuilder<AioDynamicContext>();
            if (provider == "MsSql")
            {
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(assembly));
            }
            else
            {
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly(assembly));
            }
            return new AioDynamicContext(optionsBuilder.Options, null)
            {
                Schema = provider == "MsSql" ? "dbo" : "public"
            };
        }
    }
}