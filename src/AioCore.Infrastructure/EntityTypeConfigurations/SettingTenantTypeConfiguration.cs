using AioCore.Domain.AggregatesModel.SettingTenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingTenantTypeConfiguration : IEntityTypeConfiguration<SettingTenant>
    {
        public void Configure(EntityTypeBuilder<SettingTenant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}