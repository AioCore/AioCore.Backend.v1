using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingEntityTypeConfiguration : EntityTypeConfiguration<SettingEntityType>
    {
        public override void Config(EntityTypeBuilder<SettingEntityType> builder)
        {
        }
    }
}