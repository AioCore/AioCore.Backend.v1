using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    internal class SettingFieldTypeConfiguration : EntityTypeConfiguration<SettingField>
    {
        public override void Config(EntityTypeBuilder<SettingField> builder)
        {
        }
    }
}