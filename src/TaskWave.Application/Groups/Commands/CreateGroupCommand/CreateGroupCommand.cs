
using ErrorOr;

using TaskWave.Application.Common.Security.Permissions;
using TaskWave.Application.Common.Security.Policies;
using TaskWave.Application.Common.Security.Requests;
using TaskWave.Application.Groups.Common;

namespace TaskWave.Application.Groups.Commands.CreateGroupCommand;

[Authorize(Permissions = Permission.User.Get, Policies = Policy.SelfOrAdmin)]
public record CreateGroupCommand(
    string Name
) : IAuthorizeableRequest<ErrorOr<GroupResult>>
{
    public Ulid UserId { get; set; }
}