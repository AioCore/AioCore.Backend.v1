using AioCore.API.AutofacModules;
using AioCore.Application.IntegrationEvents;
using AioCore.Application.Services;
using AioCore.Application.UnitOfWorks;
using AioCore.Infrastructure;
using AioCore.Infrastructure.Authorize;
using AioCore.Infrastructure.UnitOfWorks;
using AioCore.Shared;
using AioCore.Shared.Filters;
using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Package.AutoMapper;
using Package.DatabaseManagement;
using Package.Elasticsearch;
using Package.EventBus.EventBus;
using Package.EventBus.EventBus.Abstractions;
using Package.EventBus.EventBus.RabbitMQ;
using Package.EventBus.EventBus.ServiceBus;
using Package.EventBus.IntegrationEventLogEF;
using Package.EventBus.IntegrationEventLogEF.Services;
using Package.Localization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using Assembly = AioCore.Application.Assembly;

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
            services.AddMediatR(typeof(Assembly).GetTypeInfo().Assembly);

            services.AddHttpContextAccessor();

            services.AddAioLocalization()
                .AddCustomMvc()
                .AddCustomDbContext(_configuration)
                .AddCustomSwagger()
                .AddCustomIntegrations(_configuration)
                .AddEventBus(_configuration)
                .AddElasticsearch(_configuration)
                .AddMapper();

            services.Configure<AppSettings>(_configuration);

            services.AddAioAuthorize(_configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationModule());
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.CreateLogger<Startup>().LogDebug("Logging..."); ;

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
                .AddNewtonsoftJson();

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

            services.AddSchemaDbContext<AioDynamicContext>(
                (serviceProvider) => serviceProvider.GetRequiredService<IDatabaseInfoService>().GetDatabaseInfo(),
                typeof(Startup).GetTypeInfo().Assembly.GetName().Name);

            services.AddScoped<IAioCoreUnitOfWork, AioCoreUnitOfWork>();
            services.AddScoped<IAioDynamicUnitOfWork, AioDynamicUnitOfWork>();

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                _ => c => new IntegrationEventLogService(c));

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
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, iLifetimeScope);
                });
            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMq>(sp =>
                {
                    var subscriptionClientName = configuration["EventBus:SubscriptionClientName"];
                    var rabbitMqPersistentConnection = sp.GetRequiredService<IRabbitMqPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMq>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBus:RetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBus:RetryCount"]);
                    }

                    return new EventBusRabbitMq(rabbitMqPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
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
    }
}