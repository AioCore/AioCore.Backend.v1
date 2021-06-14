﻿using AioCore.Infrastructure;
using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AioCore.API.Factories
{
    public class AioCoreContextFactory : IDesignTimeDbContextFactory<AioCoreContext>
    {
        public AioCoreContext CreateDbContext(string[] args)
        {
            var configuration = AioCoreConfigs.Configuration();
            var optionsBuilder = new DbContextOptionsBuilder<AioCoreContext>();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("AioCore.API"));
            return new AioCoreContext(optionsBuilder.Options, null);
        }
    }
}