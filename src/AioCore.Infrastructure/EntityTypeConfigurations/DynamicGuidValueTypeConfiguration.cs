using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicGuidValueTypeConfiguration : EntityTypeConfiguration<DynamicGuidValue>
    {
        public override void Config(EntityTypeBuilder<DynamicGuidValue> builder)
        {
        }
    }
}