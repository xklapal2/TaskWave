using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TaskWave.Infrastructure.Persistence.Converters;

/// <summary>
/// Ulid to String and vice-versa converter.
/// </summary>
public class UlidToStringConverter : ValueConverter<Ulid, string>
{
    public UlidToStringConverter()
        : base(
            ulid => ulid.ToString(), // Convert Ulid to string for storage
            str => Ulid.Parse(str) // Convert string back to Ulid for usage
        )
    { }
}