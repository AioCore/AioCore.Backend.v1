using AioCore.Domain.AggregatesModel.DynamicStringAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicStringAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicStringAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicStringAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}