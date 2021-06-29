using AioCore.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AioCore.Infrastructure.EntityTypeConfigurations
{
    public abstract class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity: Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Ignore(x => x.DomainEvents);
            builder.Ignore(x => x.IndexType);
            builder.Property(x => x.CreatedBy).HasMaxLength(50);
            builder.Property(x => x.UpdatedBy).HasMaxLength(50);
            Config(builder);
        }

        public abstract void Config(EntityTypeBuilder<TEntity> builder);
    }
}
