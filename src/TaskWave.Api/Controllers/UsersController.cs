using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Api.Contracts.Users;
using TaskWave.Application.Users.Commands.CreateUserCommand;
using TaskWave.Application.Users.Common;
using TaskWave.Application.Users.Queries.ListUsers;
using TaskWave.Domain.Entities;

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

    [HttpGet]
    public async Task<IActionResult> ListUsers()
    {
        ErrorOr<List<User>> result = await mediator.Send(new ListUsersQuery());

        return result.Match(
           users => Ok(users.ConvertAll(x => x.ToDto())),
           Problem
        );
    }
}