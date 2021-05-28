using AioCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace AioCore.API.Factories
{
    public class AioCoreContextFactory : IDesignTimeDbContextFactory<AioCoreContext>
    {
        public AioCoreContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AioCoreContext>();
            optionsBuilder.UseNpgsql(b => b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            return new AioCoreContext(optionsBuilder.Options);
        }
    }
}