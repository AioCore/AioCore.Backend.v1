using AioCore.Domain.SettingAggregatesModel.SettingFormAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingFormTypeConfiguration : IEntityTypeConfiguration<SettingForm>
    {
        public void Configure(EntityTypeBuilder<SettingForm> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}