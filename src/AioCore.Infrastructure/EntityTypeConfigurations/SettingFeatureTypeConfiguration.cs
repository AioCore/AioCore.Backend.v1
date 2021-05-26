using AioCore.Domain.AggregatesModel.SettingFeatureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingFeatureTypeConfiguration : IEntityTypeConfiguration<SettingFeature>
    {
        public void Configure(EntityTypeBuilder<SettingFeature> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.Parent);
            builder.Ignore(x => x.Root);
        }
    }
}