
using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Application.Authentication.Login;

namespace TaskWave.Api.Controllers;

#region Contracts
public record LoginRequest(
    string Email,
    string Password
);

public record LoginResponse(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token
);
#endregion

public partial class AuthController
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        LoginCommand command = new(
            request.Email,
            request.Password
        );

        ErrorOr<LoginResult> authenticationResult = await mediator.Send(command);

        return authenticationResult.Match(
            result => Ok(ResultToResponse(result)),
            Problem
        );
    }

    private static LoginResponse ResultToResponse(LoginResult result)
        => new(result.Id, result.FirstName, result.LastName, result.Email, result.Token);
}