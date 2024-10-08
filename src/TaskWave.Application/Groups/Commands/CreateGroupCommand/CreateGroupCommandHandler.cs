using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Commands.CreateGroupCommand;

public class CreateGroupCommandHandler(IGroupRepository groupRepository) : IRequestHandler<CreateGroupCommand, ErrorOr<Group>>
{

    public async Task<ErrorOr<Group>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {
        if (await groupRepository.ExistsAsync(request.Name, cancellationToken))
        {
            return Error.Conflict(description: "A group with this name already exists.");
        }

        Group group = Group.Create(request.Name);

        await groupRepository.AddAsync(group, cancellationToken);

        return group;
    }
}