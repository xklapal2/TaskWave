using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskWave.Domain.Entities;
using TaskWave.Infrastructure.Persistence.Abstractions.Configurations;

namespace TaskWave.Infrastructure.Persistence.Configurations;

public class ConfigurationBuilder : EntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable($"{nameof(User)}s");

        builder
            .HasIndex(x => x.Email)
            .IsUnique();

        builder
            .Property(x => x.FirstName);
        // .HasMaxLength(ValidationConstants.MaxNameLength);

        builder
            .Property(x => x.LastName);
        // .HasMaxLength(ValidationConstants.MaxNameLength);

        builder
            .Property(x => x.Email);
        // .HasMaxLength(ValidationConstants.MaxEmailLength);
    }
}