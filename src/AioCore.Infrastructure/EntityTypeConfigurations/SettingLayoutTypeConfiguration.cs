using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingLayoutTypeConfiguration : EntityTypeConfiguration<SettingLayout>
    {
        public override void Config(EntityTypeBuilder<SettingLayout> builder)
        {
        }
    }
}