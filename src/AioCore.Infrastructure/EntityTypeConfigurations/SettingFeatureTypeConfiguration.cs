using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingFeatureTypeConfiguration : EntityTypeConfiguration<SettingFeature>
    {
        public override void Config(EntityTypeBuilder<SettingFeature> builder)
        {
            builder.Ignore(x => x.Parent);
            builder.Ignore(x => x.Root);
        }
    }
}