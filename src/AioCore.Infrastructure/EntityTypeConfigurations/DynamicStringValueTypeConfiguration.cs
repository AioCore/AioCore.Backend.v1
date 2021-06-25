using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicStringValueTypeConfiguration : IEntityTypeConfiguration<DynamicStringValue>
    {
        public void Configure(EntityTypeBuilder<DynamicStringValue> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}