using AioCore.Domain.CoreEntities;
using AioCore.Shared.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class SettingActionStepTypeConfiguration : EntityTypeConfiguration<SettingActionStep>
    {
        public override void Config(EntityTypeBuilder<SettingActionStep> builder)
        {
            builder.Property(x => x.Action)
                .HasConversion(v => v.ToString(), v => Enum.Parse<ActionDefinition>(v));
        }
    }
}
