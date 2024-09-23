using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Commands.AddGroupMember;

public class AddGroupMemberHandler(IGroupRepository groupRepository) : IRequestHandler<AddGroupMemberCommand, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(AddGroupMemberCommand request, CancellationToken cancellationToken)
    {
        if (await groupRepository.GetByIdAsync(request.GroupId, cancellationToken) is not Group group)
        {
            return Error.NotFound(description: "Group not found.");
        }

        foreach (Ulid userId in request.UserIds) // TODO: It needs to rise a GroupMemberAddedEvent so system can catch it and add GroupMember also as ProjectMemeber to all projects where group is added as ProjectGroup
        {
            if (group.AddMember(userId))
            {
                await groupRepository.UpdateAsync(group, cancellationToken);
            }
        }

        return new Success();
    }
}