namespace TaskWave.Infrastructure.Security.CurrentUserProvider;

public record CurrentUser(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email,
    IReadOnlyList<string> Permissions,
    IReadOnlyList<string> Roles
);