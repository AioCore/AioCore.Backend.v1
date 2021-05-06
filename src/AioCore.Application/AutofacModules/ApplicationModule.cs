using AioCore.Application.Behaviors;
using AioCore.Domain.AggregatesModel.SecurityUserAggregate;
using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Infrastructure.Repositories;
using Autofac;
using MediatR;
using Package.Elasticsearch;

namespace AioCore.Application.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterType<ElasticsearchService>()
                .As<IElasticsearchService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SecurityUserRepository>()
                .As<ISecurityUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SettingTenantRepository>()
                .As<ISettingTenantRepository>()
                .InstancePerLifetimeScope();
        }
    }
}