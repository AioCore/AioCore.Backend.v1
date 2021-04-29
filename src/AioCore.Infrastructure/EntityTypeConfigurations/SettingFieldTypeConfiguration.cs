using AioCore.Domain.AggregatesModel.SettingFieldAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    internal class SettingFieldTypeConfiguration : IEntityTypeConfiguration<SettingField>
    {
        public void Configure(EntityTypeBuilder<SettingField> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}