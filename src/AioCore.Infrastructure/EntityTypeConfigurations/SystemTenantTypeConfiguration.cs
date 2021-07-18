using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemTenantTypeConfiguration : EntityTypeConfiguration<SystemTenant>
    {
        public override void Config(EntityTypeBuilder<SystemTenant> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.Property(t => t.DatabaseSettingsJson).HasMaxLength(255).IsRequired();
            builder.Property(t => t.ElasticsearchSettingsJson).HasMaxLength(255).IsRequired();
        }
    }
}