using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AioCore.Infrastructure
{
    public class AioCoreContextFactory : IDesignTimeDbContextFactory<AioCoreContext>
    {
        public AioCoreContext CreateDbContext(string[] args)
        {
            var configuration = AioCoreConfigs.Configuration();
            var optionsBuilder = new DbContextOptionsBuilder<AioCoreContext>();
            optionsBuilder.UseNpgsql(configuration["ConnectionString"],
                b => b.MigrationsAssembly("AioCore.API"));
            return new AioCoreContext(optionsBuilder.Options);
        }
    }
}