using AioCore.Domain.AggregatesModel.SystemUserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemUserPolicyTypeConfiguration : IEntityTypeConfiguration<SystemUserPolicy>
    {
        public void Configure(EntityTypeBuilder<SystemUserPolicy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}