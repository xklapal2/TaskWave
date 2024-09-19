using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Api.Contracts.Users;
using TaskWave.Application.Users.Commands.CreateUserCommand;
using TaskWave.Application.Users.Common;

namespace TaskWave.Api.Controllers;

[Route("users/")]
public sealed class UsersController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserRequest request)
    {
        CreateUserCommand command = new(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        ErrorOr<UserResult> result = await mediator.Send(command);

        return result.Match(
            user => CreatedAtAction(
                actionName: nameof(CreateUser), // TODO: GetUser
                routeValues: new { UserId = user.Id },
                value: user),
            Problem
        );
    }
}