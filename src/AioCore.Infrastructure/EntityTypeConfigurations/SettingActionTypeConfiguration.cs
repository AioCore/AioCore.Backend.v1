using AioCore.Domain.CoreEntities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingActionTypeConfiguration : EntityTypeConfiguration<SettingAction>
    {
        public override void Config(EntityTypeBuilder<SettingAction> builder)
        {
            builder.HasMany(x => x.SettingActionSteps)
                .WithOne(x => x.SettingAction)
                .HasForeignKey(x => x.SettingActionId);
        }
    }
}