using AioCore.Domain.AggregatesModel.DynamicDateAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicDateAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicDateAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicDateAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}