using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskWave.Infrastructure.Persistence.Ulids;

public static class UlidConfig
{
    public static void ConfigureUlid<TEntity>(EntityTypeBuilder<TEntity> builder, Expression<Func<TEntity, object?>> propertyExpression)
        where TEntity : class
    {
        builder.Property(propertyExpression)
            .HasConversion(new UlidToStringConverter());
    }

    public static void ConfigureUlid<TEntity, TValueObject>(
        OwnedNavigationBuilder<TEntity, TValueObject> builder,
        Expression<Func<TValueObject, object?>> propertyExpression
    )
        where TEntity : class
        where TValueObject : class
    {
        builder.Property(propertyExpression)
            .HasConversion(new UlidToStringConverter())
            .ValueGeneratedNever();
    }
}