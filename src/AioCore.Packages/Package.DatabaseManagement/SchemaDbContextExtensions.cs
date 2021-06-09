using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Package.DatabaseManagement
{
    public static class SchemaDbContextExtensions
    {
        public static IServiceCollection AddSchemaDbContext<TContext>(this IServiceCollection services
            , Func<IServiceProvider, DatabaseInfo> dbInfoFactory
            , string assemblyName
            , ServiceLifetime contextLifetime = ServiceLifetime.Scoped
            , ServiceLifetime optionsLifetime = ServiceLifetime.Scoped) where TContext : SchemaDbContext
        {
            services.AddDbContext<TContext>((serviceProvider, options) =>
            {
                var dbInfo = dbInfoFactory(serviceProvider);
                options.UseSql(dbInfo, assemblyName);
            }, contextLifetime, optionsLifetime);

            return services;
        }

        private static DbContextOptionsBuilder UseSql(this DbContextOptionsBuilder optionsBuilder, DatabaseInfo dbInfo, string assemblyName)
        {
            var connectionString = GetConnectionString();

            return dbInfo.DatabaseType switch
            {
                DatabaseType.PostgresSql => optionsBuilder.UseNpgsql(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbInfo.Schema);
                }),
                _ => optionsBuilder.UseSqlServer(connectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(assemblyName);
                    sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    sqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, dbInfo.Schema);
                }),
            };

            string GetConnectionString()
            {
                if (dbInfo == null) throw new ArgumentNullException(nameof(dbInfo));

                return dbInfo.DatabaseType switch
                {
                    DatabaseType.PostgresSql => $"Host={dbInfo.Server};User ID={dbInfo.User};Password={dbInfo.Password};Database={dbInfo.Database};Pooling=true;",
                    _ => $"Server={dbInfo.Server};User ID={dbInfo.User};Password={dbInfo.Password};Database={dbInfo.Database};Pooling=true;",
                };
            }
        }
    }
}
