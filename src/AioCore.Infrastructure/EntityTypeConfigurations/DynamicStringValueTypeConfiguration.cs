using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicStringValueTypeConfiguration : EntityTypeConfiguration<DynamicStringValue>
    {
        public override void Config(EntityTypeBuilder<DynamicStringValue> builder)
        {
        }
    }
}