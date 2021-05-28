using AioCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace AioCore.API.Factories
{
    public class AioDynamicContextFactory : IDesignTimeDbContextFactory<AioDynamicContext>
    {
        public AioDynamicContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AioDynamicContext>();
            optionsBuilder.UseNpgsql(b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            return new AioDynamicContext(optionsBuilder.Options, null)
            {
                Schema = "dbo"
            };
        }
    }
}