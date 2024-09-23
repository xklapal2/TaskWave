using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Queries.ListGroups;

public class ListGroupsQueryHandler(IGroupRepository groupRepository) : IRequestHandler<ListGroupsQuery, ErrorOr<List<Group>>>
{
    public async Task<ErrorOr<List<Group>>> Handle(ListGroupsQuery request, CancellationToken cancellationToken)
    {
        return await groupRepository.ListGroupsAsync(cancellationToken);
    }
}