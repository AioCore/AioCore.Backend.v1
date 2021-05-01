﻿using AioCore.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Package.EventBus.IntegrationEventLogEF;

namespace AioCore.API.Factories
{
    public class IntegrationEventLogContextFactory : IDesignTimeDbContextFactory<IntegrationEventLogContext>
    {
        public IntegrationEventLogContext CreateDbContext(string[] args)
        {
            var configuration = AioCoreConfigs.Configuration();
            var optionsBuilder = new DbContextOptionsBuilder<IntegrationEventLogContext>();
            optionsBuilder.UseNpgsql(configuration["ConnectionString"],
                b => b.MigrationsAssembly("AioCore.API"));
            return new IntegrationEventLogContext(optionsBuilder.Options);
        }
    }
}