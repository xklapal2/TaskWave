using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;

namespace TaskWave.Application.Groups.Queries.GroupDetail;

public class GroupDetailQueryHandler(IGroupRepository groupRepository) : IRequestHandler<GroupDetailQuery, ErrorOr<GroupDetail>>
{
    public async Task<ErrorOr<GroupDetail>> Handle(GroupDetailQuery request, CancellationToken cancellationToken)
    {
        if (await groupRepository.GroupDetailAsync(request.Id, cancellationToken) is not GroupDetail detail)
        {
            return Error.NotFound(description: "Group not found");
        }

        return detail;
    }
}