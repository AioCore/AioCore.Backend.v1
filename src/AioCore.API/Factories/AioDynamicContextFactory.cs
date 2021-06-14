using AioCore.Infrastructure.DbContexts;
using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace AioCore.API.Factories
{
    public class AioDynamicContextFactory : IDesignTimeDbContextFactory<AioDynamicContext>
    {
        public AioDynamicContext CreateDbContext(string[] args)
        {
            var configuration = AioCoreConfigs.Configuration();
            var optionsBuilder = new DbContextOptionsBuilder<AioDynamicContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            return new AioDynamicContext(optionsBuilder.Options, null)
            {
                Schema = "public"
            };
        }
    }
}