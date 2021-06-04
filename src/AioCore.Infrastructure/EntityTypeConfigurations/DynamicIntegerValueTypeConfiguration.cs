using AioCore.Domain.AggregatesModel.DynamicAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicIntegerValueTypeConfiguration : IEntityTypeConfiguration<DynamicIntegerValue>
    {
        public void Configure(EntityTypeBuilder<DynamicIntegerValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}