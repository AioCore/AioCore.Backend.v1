using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingDomTypeConfiguration : EntityTypeConfiguration<SettingDom>
    {
        public override void Config(EntityTypeBuilder<SettingDom> builder)
        {
        }
    }
}