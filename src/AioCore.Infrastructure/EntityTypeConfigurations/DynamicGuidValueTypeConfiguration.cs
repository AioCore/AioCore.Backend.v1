using AioCore.Domain.AggregatesModel.DynamicGuidAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicGuidValueTypeConfiguration : IEntityTypeConfiguration<DynamicGuidValue>
    {
        public void Configure(EntityTypeBuilder<DynamicGuidValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}