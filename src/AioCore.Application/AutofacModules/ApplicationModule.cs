using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using AioCore.Infrastructure.Repositories;
using Autofac;

namespace AioCore.Application.AutofacModules
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SettingTenantRepository>()
                .As<ISettingTenantRepository>()
                .InstancePerLifetimeScope();
        }
    }
}