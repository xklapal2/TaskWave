using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Application.Users.Commands.CreateUserCommand;
using TaskWave.Application.Users.Common;

namespace TaskWave.Api.Controllers;

#region Contracts
public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password
);

public record CreateUserResponse(
    Ulid Id,
    string FirstName,
    string LastName,
    string Email
);
#endregion

public partial class UsersController
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