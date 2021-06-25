using AioCore.Domain.DynamicEntities;
using AioCore.Shared.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public class DynamicAttributeTypeConfiguration : IEntityTypeConfiguration<DynamicAttribute>
    {
        public void Configure(EntityTypeBuilder<DynamicAttribute> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Property(x => x.DataType)
                .HasConversion(v => v.ToString(), v => Enum.Parse<DataType>(v));
        }
    }
}