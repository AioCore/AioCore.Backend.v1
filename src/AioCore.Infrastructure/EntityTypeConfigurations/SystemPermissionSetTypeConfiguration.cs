using AioCore.Domain.SystemAggregatesModel.SystemPermissionSetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemPermissionSetTypeConfiguration : IEntityTypeConfiguration<SystemPermissionSet>
    {
        public void Configure(EntityTypeBuilder<SystemPermissionSet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}