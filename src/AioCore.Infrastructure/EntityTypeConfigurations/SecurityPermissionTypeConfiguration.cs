using AioCore.Domain.AggregatesModel.SecurityPermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SecurityPermissionTypeConfiguration : IEntityTypeConfiguration<SecurityPermission>
    {
        public void Configure(EntityTypeBuilder<SecurityPermission> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}