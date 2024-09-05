
using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Application.Groups.Commands.Common;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Commands.CreateGroup;

public class CreateGroupHandler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, ErrorOr<GroupResult>>
{
    public async Task<ErrorOr<GroupResult>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        if (await groupRepository.ExistsAsync(request.Name, cancellationToken))
        {
            return Error.Conflict(description: "A group with this name already exists.");
        }

        Group group = Group.Create(request.Name);

        await groupRepository.AddAsync(group, cancellationToken);

        return GroupResult.FromGroup(group);
    }
}