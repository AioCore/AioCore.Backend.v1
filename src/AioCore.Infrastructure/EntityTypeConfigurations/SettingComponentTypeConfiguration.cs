using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingComponentTypeConfiguration : IEntityTypeConfiguration<SettingComponent>
    {
        public void Configure(EntityTypeBuilder<SettingComponent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
        }
    }
}