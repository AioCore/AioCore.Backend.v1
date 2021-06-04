using AioCore.Domain.SystemAggregatesModel.SystemPermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemPermissionTypeConfiguration : IEntityTypeConfiguration<SystemPermission>
    {
        public void Configure(EntityTypeBuilder<SystemPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}