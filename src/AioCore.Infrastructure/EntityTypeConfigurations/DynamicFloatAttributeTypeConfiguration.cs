using AioCore.Domain.AggregatesModel.DynamicFloatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicFloatAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicFloatAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicFloatAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}