namespace TaskWave.Api.Contracts.Authentication;

public record LoginResponse(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);