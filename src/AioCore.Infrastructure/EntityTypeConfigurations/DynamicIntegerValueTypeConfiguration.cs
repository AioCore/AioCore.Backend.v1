using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicIntegerValueTypeConfiguration : EntityTypeConfiguration<DynamicIntegerValue>
    {
        public override void Config(EntityTypeBuilder<DynamicIntegerValue> builder)
        {
        }
    }
}