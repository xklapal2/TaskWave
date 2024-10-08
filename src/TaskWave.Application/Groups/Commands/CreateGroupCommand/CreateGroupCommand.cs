
using ErrorOr;

using TaskWave.Application.Common.Security.Permissions;
using TaskWave.Application.Common.Security.Policies;
using TaskWave.Application.Common.Security.Requests;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Groups.Commands.CreateGroupCommand;

[Authorize(Permissions = Permission.Group.Create, Policies = Policy.SelfOrAdmin)]
public record CreateGroupCommand(
    string Name
) : IAuthorizeableRequest<ErrorOr<Group>>
{
    public Ulid AuthorizedUserId { get; set; }
}