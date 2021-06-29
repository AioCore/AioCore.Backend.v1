using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingActionTypeConfiguration : EntityTypeConfiguration<SettingAction>
    {
        public override void Config(EntityTypeBuilder<SettingAction> builder)
        {
            builder.HasMany(x => x.ActionSteps)
                .WithOne(x => x.Action)
                .HasForeignKey(x => x.ActionId);
        }
    }
}