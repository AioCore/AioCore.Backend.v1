using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingViewTypeConfiguration : EntityTypeConfiguration<SettingView>
    {
        public override void Config(EntityTypeBuilder<SettingView> builder)
        {
        }
    }
}