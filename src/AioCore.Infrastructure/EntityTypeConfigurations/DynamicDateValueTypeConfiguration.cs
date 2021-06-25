using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicDateValueTypeConfiguration : EntityTypeConfiguration<DynamicDateValue>
    {
        public override void Config(EntityTypeBuilder<DynamicDateValue> builder)
        {
            
        }
    }
}