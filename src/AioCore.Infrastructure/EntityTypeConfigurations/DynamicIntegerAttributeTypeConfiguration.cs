using AioCore.Domain.AggregatesModel.DynamicIntegerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicIntegerAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicIntegerAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicIntegerAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}