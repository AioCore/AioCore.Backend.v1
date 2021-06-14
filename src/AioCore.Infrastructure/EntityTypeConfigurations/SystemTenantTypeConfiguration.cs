using AioCore.Domain.SystemAggregatesModel.SystemTenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemTenantTypeConfiguration : IEntityTypeConfiguration<SystemTenant>
    {
        public void Configure(EntityTypeBuilder<SystemTenant> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Property(t => t.Name).HasMaxLength(50).IsRequired();
            builder.Property(t => t.DatabaseInfo).HasMaxLength(255).IsRequired();
            builder.Property(t => t.ElasticsearchInfo).HasMaxLength(255).IsRequired();
        }
    }
}