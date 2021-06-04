using AioCore.Domain.SettingAggregatesModel.SettingDomAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingDomTypeConfiguration : IEntityTypeConfiguration<SettingDom>
    {
        public void Configure(EntityTypeBuilder<SettingDom> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}