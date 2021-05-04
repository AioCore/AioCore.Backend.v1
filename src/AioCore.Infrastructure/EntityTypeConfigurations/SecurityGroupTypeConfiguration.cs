using AioCore.Domain.AggregatesModel.SecurityGroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SecurityGroupTypeConfiguration : IEntityTypeConfiguration<SecurityGroup>
    {
        public void Configure(EntityTypeBuilder<SecurityGroup> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}