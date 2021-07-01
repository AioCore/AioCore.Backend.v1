using AioCore.Application;
using AioCore.Application.IntegrationEvents;
using AioCore.Infrastructure.Authorize;
using AioCore.Infrastructure.DbContexts;
using AioCore.Infrastructure.Repositories;
using AioCore.Infrastructure.Services;
using AioCore.Infrastructure.UnitOfWorks;
using AioCore.Mediator;
using AioCore.Shared;
using AioCore.Shared.Filters;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Package.AutoMapper;
using Package.DatabaseManagement;
using Package.Elasticsearch;
using Package.EventBus.EventBus;
using Package.EventBus.EventBus.Abstractions;
using Package.EventBus.EventBus.RabbitMQ;
using Package.EventBus.EventBus.ServiceBus;
using Package.EventBus.IntegrationEventLogEF;
using Package.EventBus.IntegrationEventLogEF.Services;
using Package.Extensions;
using Package.FileServer;
using Package.Localization;
using Package.Redis;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AioCore.Infrastructure.Repositories.Abstracts;
using AioCore.Infrastructure.Services.Abstracts;
using AioCore.Infrastructure.UnitOfWorks.Abstracts;
using AioCore.Shared.Abstracts;
using Plugin.ActionProcessor;
using Plugin.ViewRender;

namespace AioCore.API
{
    public sealed class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAioLocalization()
                .AddCustomMvc()
                .AddCustomDbContext(_configuration)
                .AddCustomSwagger()
                .AddCustomIntegrations(_configuration)
                .AddEventBus(_configuration)
                .AddMapper();

            services.Configure<AppSettings>(_configuration);

            services.AddAioAuthorize(_configuration);

            services.AddCacheManager();

            services.RegisterAllServices();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();

            app.UseAioLocalization();

            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Document API aioc.vn");
                });

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });

            app.UseEventBus();
        }
    }

    public static class StartupExtensions
    {
        private static List<CultureInfo> SupportedCultures => new()
        {
            new CultureInfo("en-US"),
            new CultureInfo("vi-VN")
        };

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            return app;
        }

        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            services.AddControllers(options =>
                {
                    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            return services;
        }

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

            services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(15, TimeSpan.FromSeconds(30), null);
                    });
            });

            services.AddSchemaDbContext<AioDynamicContext>(s => s.GetRequiredService<IDatabaseInfoService>().GetDatabaseInfo());

            services.AddUnitOfWork<IAioCoreUnitOfWork, AioCoreUnitOfWork, AioCoreContext>()
                    .AddUnitOfWork<IAioDynamicUnitOfWork, AioDynamicUnitOfWork, AioDynamicContext>()
                    .AddScoped<IAioDynamicUnitOfWorkFactory, AioDynamicUnitOfWorkFactory>();

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<EnumSchemaFilter>();
                options.OperationFilter<FormFileSwaggerFilter>();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Document API aioc.vn",
                    Version = "v1"
                });
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(_ => c => new IntegrationEventLogService(c));

            services.AddTransient<IAioIntegrationEventService, AioIntegrationEventService>();

            if (configuration.GetValue<bool>("EventBus:AzureServiceBusEnabled"))
            {
                services.AddSingleton<IServiceBusPersisterConnection>(_ =>
                {
                    var serviceBusConnectionString = configuration["EventBus:Connection"];
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);
                    var subscriptionClientName = configuration["EventBus:SubscriptionClientName"];

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, subscriptionClientName);
                });
            }
            else
            {
                services.AddSingleton<IRabbitMqPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMqPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration["EventBus:Connection"],
                        DispatchConsumersAsync = true
                    };

                    if (!string.IsNullOrEmpty(configuration["EventBus:UserName"]))
                    {
                        factory.UserName = configuration["EventBus:UserName"];
                    }

                    if (!string.IsNullOrEmpty(configuration["EventBus:Password"]))
                    {
                        factory.Password = configuration["EventBus:Password"];
                    }

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBus:RetryCount"]);
                    }

                    return new DefaultRabbitMqPersistentConnection(factory, logger, retryCount);
                });
            }

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("EventBus:AzureServiceBusEnabled"))
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>();
            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMq>();
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper.RegisterMap());
            return services;
        }

        public static IServiceCollection RegisterAllServices(this IServiceCollection services)
        {
            var asms = AssemblyHelper.Assemblies.ToArray();

            services.AddMediator(asms);

            services.AddTransient<IDateTime, CustomDateTime>();
            services.AddSingleton<ICurrentUser, CurrentUser>();
            services.AddValidatorsFromAssemblies(asms);

            services.AddScoped<IElasticClientFactory, ElasticClientFactory>();
            services.AddScoped<IElasticsearchService, ElasticsearchService>();
            services.AddScoped<IFileServerService, FileServerService>();

            services.AddViewRender();
            services.AddDynamicAction();

            foreach (var type in AssemblyHelper.ExportTypes)
            {
                if (!type.Name.EndsWith("Repository") && !type.Name.EndsWith("Service")) continue;
                foreach (var serviceType in type.GetInterfaces().Where(t => !t.Name.StartsWith("System")))
                {
                    services.TryAddScoped(serviceType, type);
                }
            }
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
    }
}