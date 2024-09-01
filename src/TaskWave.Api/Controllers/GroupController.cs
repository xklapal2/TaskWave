using Microsoft.AspNetCore.Components;

namespace TaskWave.Api.Controllers;

[Route("groups/")]
public class GroupController : ApiController
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