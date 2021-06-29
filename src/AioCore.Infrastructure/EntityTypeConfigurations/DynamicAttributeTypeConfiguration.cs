using AioCore.Domain.DynamicEntities;
using AioCore.Shared.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicAttributeTypeConfiguration : EntityTypeConfiguration<DynamicAttribute>
    {
        public override void Config(EntityTypeBuilder<DynamicAttribute> builder)
        {
            builder.Property(x => x.DataType)
                .HasConversion(v => v.ToString(), v => Enum.Parse<DataType>(v));
        }
    }
}