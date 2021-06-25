using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingFilterTypeConfiguration : EntityTypeConfiguration<SettingFilter>
    {
        public override void Config(EntityTypeBuilder<SettingFilter> builder)
        {
        }
    }
}
