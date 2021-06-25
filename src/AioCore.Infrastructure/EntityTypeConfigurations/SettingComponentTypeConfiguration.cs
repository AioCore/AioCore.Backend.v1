using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingComponentTypeConfiguration : EntityTypeConfiguration<SettingComponent>
    {
        public override void Config(EntityTypeBuilder<SettingComponent> builder)
        {
        }
    }
}