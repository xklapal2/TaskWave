using ErrorOr;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Application.Groups.Commands.Common;

using TaskWave.Application.Groups.Commands.CreateGroup;

namespace TaskWave.Api.Controllers;

# region Contracts

public record CreateGroupRequest(
    string Name
);

public record CreateGroupResponse(
    Ulid Id,
    string Name
);
#endregion

public partial class GroupController
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        CreateGroupCommand command = new(
            request.Name
        );

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
