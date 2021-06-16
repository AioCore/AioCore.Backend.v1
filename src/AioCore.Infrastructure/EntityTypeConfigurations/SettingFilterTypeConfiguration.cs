using AioCore.Domain.SettingAggregatesModel.SettingFilterAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingFilterTypeConfiguration : IEntityTypeConfiguration<SettingFilter>
    {
        public void Configure(EntityTypeBuilder<SettingFilter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
