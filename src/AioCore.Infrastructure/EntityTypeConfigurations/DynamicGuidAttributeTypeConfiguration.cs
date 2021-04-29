using AioCore.Domain.AggregatesModel.DynamicGuidAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicGuidAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicGuidAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicGuidAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}