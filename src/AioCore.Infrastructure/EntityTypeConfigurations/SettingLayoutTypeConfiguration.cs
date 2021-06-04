using AioCore.Domain.SettingAggregatesModel.SettingLayoutAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingLayoutTypeConfiguration : IEntityTypeConfiguration<SettingLayout>
    {
        public void Configure(EntityTypeBuilder<SettingLayout> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}