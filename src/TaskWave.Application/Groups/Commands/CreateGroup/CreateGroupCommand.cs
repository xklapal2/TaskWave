using ErrorOr;

using TaskWave.Application.Common.Security.Requests;
using TaskWave.Application.Groups.Commands.Common;

namespace TaskWave.Application.Groups.Commands.CreateGroup;

public record CreateGroupCommand(
    string Name
) : IAuthorizeableRequest<ErrorOr<GroupResult>>
{
    public Ulid AuthUserId { get; set; }
}