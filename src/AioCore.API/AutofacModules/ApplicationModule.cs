using AioCore.Application.Behaviors;
using AioCore.Infrastructure.Repositories;
using Autofac;
using MediatR;
using Package.Elasticsearch;
using Package.Extensions;
using Package.FileServer;
using System.Linq;
using AioCore.Application.Repositories;
using Package.ViewRender;

namespace AioCore.API.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.Assemblies.ToArray();
            builder.RegisterGeneric(typeof(LoggingBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(TransactionBehaviour<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.RegisterType<FileServerService>()
                .As<IFileServerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ElasticsearchService>()
                .As<IElasticsearchService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SystemUserRepository>()
                .As<ISystemUserRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SettingLayoutRepository>()
                .As<ISettingLayoutRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SettingFeatureRepository>()
                .As<ISettingFeatureRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SystemTenantRepository>()
                .As<ISettingTenantRepository>()
                .InstancePerLifetimeScope();

            //All Repositories and Services
            builder.RegisterAssemblyTypes(assemblies).Where(t => t.Name.EndsWith("Repository") || t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //All ViewEngine Processors
            builder.RegisterType<HtmlBuilder>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<ViewRenderFactory>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(IViewRenderProcessor).IsAssignableFrom(t))
                .As<IViewRenderProcessor>()
                .InstancePerLifetimeScope();
        }
    }
}