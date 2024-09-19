using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskWave.Domain.Common.Abstractions;
using TaskWave.Infrastructure.Persistence.Ulids;

namespace TaskWave.Infrastructure.Persistence.Configurations;

public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(g => g.Id);

        UlidConfig.ConfigureUlid(builder, g => g.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();
    }
}