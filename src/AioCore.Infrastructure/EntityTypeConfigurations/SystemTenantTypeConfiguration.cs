using AioCore.Domain.AggregatesModel.SystemTenantAggregate;
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
        }
    }
}