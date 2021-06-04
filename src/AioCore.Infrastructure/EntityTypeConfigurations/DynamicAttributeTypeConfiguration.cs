using AioCore.Domain.DynamicAggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}