using AioCore.Application.DynamicAction;
using AioCore.Application.DynamicView;
using AioCore.Application.Plugin;
using AioCore.Application.Repositories;
using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Infrastructure.DbContexts;
using AioCore.Infrastructure.Repositories;
using AioCore.Infrastructure.Services;
using AioCore.Infrastructure.UnitOfWorks;
using AioCore.Shared.Abstracts;
using FluentValidation;
using McMaster.NETCore.Plugins;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Package.DatabaseManagement;
using Package.Elasticsearch;
using Package.Extensions;
using Package.FileServer;
using Package.Mediator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AioCore.API
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AioCoreContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), b =>
                {
                    b.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                    b.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                });
            });

            services.AddSchemaDbContext<AioDynamicContext>(s => s.GetRequiredService<IDatabaseInfoService>().GetDatabaseInfo());

            services.AddUnitOfWork<IAioCoreUnitOfWork, AioCoreUnitOfWork, AioCoreContext>()
                    .AddUnitOfWork<IAioDynamicUnitOfWork, AioDynamicUnitOfWork, AioDynamicContext>()
                    .AddScoped<IAioDynamicUnitOfWorkFactory, AioDynamicUnitOfWorkFactory>();

            return services;
        }

        public static IServiceCollection AddUnitOfWork<TService, TImplementation, TDbContext>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
            where TDbContext : DbContext
        {
            var properties = typeof(TImplementation).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(t => typeof(IRepository).IsAssignableFrom(t.PropertyType) && t.PropertyType.IsGenericType)
                .ToList();

            services.AddScoped<TImplementation>();
            services.AddScoped<TService, TImplementation>(s =>
            {
                var instance = s.GetService<TImplementation>();
                foreach (var prop in properties)
                {
                    prop.SetValue(instance, s.GetService(prop.PropertyType));
                }
                return instance;
            });

            foreach (var prop in properties)
            {
                var genericType = prop.PropertyType.GetGenericArguments()[0];
                var implType = typeof(Repository<>).MakeGenericType(genericType);
                services.AddScoped(prop.PropertyType, s => Activator.CreateInstance(implType, s.GetService<TDbContext>()));
            }

            return services;
        }

        public static IServiceCollection RegisterServices(this IServiceCollection services, IHostEnvironment hostEnvironment)
        {
            var asms = AssemblyHelper.Assemblies.Concat(LoadPlugins(hostEnvironment)).ToArray();
            var exportedTypes = asms.SelectMany(t => t.GetExportedTypes())
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .ToList();

            services.AddMediator(asms);

            services.AddTransient<IDateTime, CustomDateTime>();
            services.AddSingleton<ICurrentUser, CurrentUser>();
            services.AddValidatorsFromAssemblies(asms);

            services.AddScoped<IElasticClientFactory, ElasticClientFactory>();
            services.AddScoped<IElasticsearchService, ElasticsearchService>();
            services.AddScoped<IFileServerService, FileServerService>();

            services.AddScoped<HtmlBuilder>();
            services.AddScoped<ViewRenderFactory>();
            foreach (var type in exportedTypes.Where(t => typeof(IViewRenderProcessor).IsAssignableFrom(t)))
            {
                services.AddScoped(typeof(IViewRenderProcessor), type);
            }

            services.AddScoped<ActionFactory>();
            foreach (var type in exportedTypes.Where(t => typeof(IActionProcessor).IsAssignableFrom(t)))
            {
                services.AddScoped(typeof(IActionProcessor), type);
            }

            foreach (var type in exportedTypes)
            {
                if (!type.Name.EndsWith("Repository") && !type.Name.EndsWith("Service")) continue;
                foreach (var serviceType in type.GetInterfaces().Where(t => !t.Name.StartsWith("System")))
                {
                    services.TryAddScoped(serviceType, type);
                }
            }

            return services;
        }

        private static IEnumerable<Assembly> LoadPlugins(IHostEnvironment hostEnvironment)
        {
            if (hostEnvironment.IsDevelopment())
            {
                var rootFolder = Path.Combine(Path.GetDirectoryName(hostEnvironment.ContentRootPath), "AioCore.Plugins");
                return Directory.GetDirectories(rootFolder)
                    .Select(t =>
                    {
                        var pluginDll = Path.Combine(t, "bin", "Debug", "net6.0", Path.GetFileName(t)) + ".dll";
                        var loader = PluginLoader.CreateFromAssemblyFile(pluginDll, sharedTypes: new[] { typeof(IPlugin) });
                        return loader.LoadDefaultAssembly();
                    });
            }
            else
            {
                var rootFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AioCore.Plugins");
                return Directory.GetFiles(rootFolder, "Plugin.*.dll")
                    .Select(pluginDll =>
                    {
                        var loader = PluginLoader.CreateFromAssemblyFile(pluginDll, sharedTypes: new[] { typeof(IPlugin) });
                        return loader.LoadDefaultAssembly();
                    });
            }
        }
    }
}