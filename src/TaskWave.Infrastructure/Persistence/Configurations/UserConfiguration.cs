using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using TaskWave.Domain.Entities;
using TaskWave.Infrastructure.Persistence.Converters;

namespace TaskWave.Infrastructure.Persistence.Configurations;

public class ConfigurationBuilder : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable($"{nameof(User)}s");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasConversion(new UlidToStringConverter())
            .ValueGeneratedNever();

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

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