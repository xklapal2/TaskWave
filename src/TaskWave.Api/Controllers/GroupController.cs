using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Api.Contracts.Groups;
using TaskWave.Application.Groups.Commands.CreateGroupCommand;
using TaskWave.Application.Groups.Common;

namespace TaskWave.Api.Controllers;

[Route("groups/")]
public class GroupController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        CreateGroupCommand command = new(request.Name);

        ErrorOr<GroupResult> result = await mediator.Send(command);

        return result.Match(
            group => CreatedAtAction(
                actionName: nameof(CreateGroup), // TODO: GetGroup
                routeValues: new { group.GroupId },
                value: group),
            Problem
        );
    }
}