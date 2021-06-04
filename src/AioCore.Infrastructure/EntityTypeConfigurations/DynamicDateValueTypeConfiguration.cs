using AioCore.Domain.DynamicAggregatesModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicDateValueTypeConfiguration : IEntityTypeConfiguration<DynamicDateValue>
    {
        public void Configure(EntityTypeBuilder<DynamicDateValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}