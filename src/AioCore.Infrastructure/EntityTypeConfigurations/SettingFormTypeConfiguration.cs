using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingFormTypeConfiguration : EntityTypeConfiguration<SettingForm>
    {
        public override void Config(EntityTypeBuilder<SettingForm> builder)
        {
        }
    }
}