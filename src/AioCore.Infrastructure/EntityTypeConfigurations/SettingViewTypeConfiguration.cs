using AioCore.Domain.SettingAggregatesModel.SettingViewAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingViewTypeConfiguration : IEntityTypeConfiguration<SettingView>
    {
        public void Configure(EntityTypeBuilder<SettingView> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}