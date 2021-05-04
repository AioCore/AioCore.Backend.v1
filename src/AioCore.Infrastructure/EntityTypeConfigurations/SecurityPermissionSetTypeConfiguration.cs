using AioCore.Domain.AggregatesModel.SecurityPermissionSetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SecurityPermissionSetTypeConfiguration : IEntityTypeConfiguration<SecurityPermissionSet>
    {
        public void Configure(EntityTypeBuilder<SecurityPermissionSet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}