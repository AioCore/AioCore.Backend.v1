using AioCore.Domain.AggregatesModel.DynamicFloatAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicFloatValueTypeConfiguration : IEntityTypeConfiguration<DynamicFloatValue>
    {
        public void Configure(EntityTypeBuilder<DynamicFloatValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}