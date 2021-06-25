using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SystemPolicyTypeConfiguration : IEntityTypeConfiguration<SystemPolicy>
    {
        public void Configure(EntityTypeBuilder<SystemPolicy> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}