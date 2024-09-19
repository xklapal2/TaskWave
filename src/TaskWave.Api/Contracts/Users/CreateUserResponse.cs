namespace TaskWave.Api.Contracts.Users;

public record CreateUserResponse(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email
);