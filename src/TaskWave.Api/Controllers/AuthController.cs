using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TaskWave.Api.Contracts.Authentication;

using TaskWave.Application.Authentication.Login;

namespace TaskWave.Api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthController(ISender mediator) : ApiController
{
    [HttpPost("login")]
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

    private static LoginResponse ResultToResponse(LoginResult result) => new(result.Id, result.FirstName, result.LastName, result.Email, result.Token);
}