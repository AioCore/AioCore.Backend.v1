using AioCore.Domain.DynamicEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicEntityTypeConfiguration : EntityTypeConfiguration<DynamicEntity>
    {
        public override void Config(EntityTypeBuilder<DynamicEntity> builder)
        {
        }
    }
}