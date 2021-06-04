using AioCore.Domain.SettingAggregatesModel.SettingEntityAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingEntityTypeConfiguration : IEntityTypeConfiguration<SettingEntityType>
    {
        public void Configure(EntityTypeBuilder<SettingEntityType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}