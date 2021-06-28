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
            builder.Property(x => x.StepType)
                .HasConversion(v => v.ToString(), v => Enum.Parse<StepType>(v));
            builder.Property(x => x.Container)
                .HasConversion(v => v.ToString(), v => Enum.Parse<ActionContainer>(v));
            builder.Property(x => x.InitParamType)
                .HasConversion(v => v.ToString(), v => Enum.Parse<InitParamType>(v));
            builder.HasOne(x => x.TargetType)
                .WithOne()
                .HasForeignKey<SettingEntityType>(x => x.Id);
        }
    }
}
