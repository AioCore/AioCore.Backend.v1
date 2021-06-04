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
            builder.Property(t => t.Server).HasMaxLength(50).IsRequired();
            builder.Property(t => t.User).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Database).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(50).IsRequired();
            builder.Property(t => t.Schema).HasMaxLength(15).IsRequired();
            builder.Property(t => t.DatabaseType).HasMaxLength(15).IsRequired();
        }
    }
}