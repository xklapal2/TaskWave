using ErrorOr;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using TaskWave.Api.Contracts.Groups;
using TaskWave.Api.Contracts.Groups.Common;
using TaskWave.Application.Groups.Commands.AddGroupMember;
using TaskWave.Application.Groups.Commands.CreateGroupCommand;
using TaskWave.Application.Groups.Queries.ListGroups;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Api.Controllers;

[Route("groups/")]
public class GroupController(IMediator mediator) : ApiController
{
    [HttpPost]
    public async Task<IActionResult> CreateGroup(CreateGroupRequest request)
    {
        CreateGroupCommand command = new(request.Name);

        ErrorOr<Group> result = await mediator.Send(command);

        return result.Match(
            group => CreatedAtAction(
                actionName: nameof(CreateGroup), // TODO: GetGroup
                routeValues: new { group.Id },
                value: group.ToDto()),
            Problem
        );
    }

    [HttpPost]
    [Route("{groupId}/addMembers")]
    public async Task<IActionResult> AddGroupMembers(AddGroupMembersRequest request)
    {
        AddGroupMemberCommand command = new(request.GroupId, request.UserIds);

        ErrorOr<Success> result = await mediator.Send(command);

        return result.Match(
            _ => NoContent(),
            Problem
        );
    }

    [HttpGet]
    public async Task<IActionResult> ListGroups()
    {
        ErrorOr<List<Group>> result = await mediator.Send(new ListGroupsQuery());

        return result.Match(
           groups => Ok(groups.ConvertAll(x => x.ToDto())),
           Problem
        );
    }
}