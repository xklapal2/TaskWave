namespace TaskWave.Api.Common.Http.RouteConstraints;

/// <summary>
/// A custom route constraint for validating ULID (Universally Unique Lexicographically Sortable Identifier) values.
/// This constraint checks if the route value corresponding to the specified route key is a non-null, valid ULID string.
/// </summary>
public class UlidRouteConstraint : IRouteConstraint
{
    public bool Match(
        HttpContext? httpContext,
        IRouter? route,
        string routeKey,
        RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        return values.TryGetValue(routeKey, out object? value) &&
                value is string strValue &&
                Ulid.TryParse(strValue.ToString(), out _);
    }
}