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

        if (group.AddMember(request.UserId))
        {
            await groupRepository.UpdateAsync(group, cancellationToken);
        }

        return new Success();
    }
}