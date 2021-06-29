using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicFloatValueTypeConfiguration : EntityTypeConfiguration<DynamicFloatValue>
    {
        public override void Config(EntityTypeBuilder<DynamicFloatValue> builder)
        {
        }
    }
}