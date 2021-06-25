using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemTenantTypeConfiguration : EntityTypeConfiguration<SystemTenant>
    {
        public override void Config(EntityTypeBuilder<SystemTenant> builder)
        {
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.Property(t => t.DatabaseInfo).HasMaxLength(255).IsRequired();
            builder.Property(t => t.ElasticsearchInfo).HasMaxLength(255).IsRequired();
        }
    }
}