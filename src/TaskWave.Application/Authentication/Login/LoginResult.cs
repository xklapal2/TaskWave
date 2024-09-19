namespace TaskWave.Application.Authentication.Login;

public record LoginResult(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);