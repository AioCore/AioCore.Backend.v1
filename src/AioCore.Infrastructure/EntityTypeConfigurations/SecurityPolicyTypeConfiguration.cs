using AioCore.Domain.AggregatesModel.SecurityPolicyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SecurityPolicyTypeConfiguration : IEntityTypeConfiguration<SecurityPolicy>
    {
        public void Configure(EntityTypeBuilder<SecurityPolicy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}