using ErrorOr;

using TaskWave.Application.Common.Security.Policies;
using TaskWave.Application.Common.Security.Permissions;

using TaskWave.Application.Common.Security.Requests;

namespace TaskWave.Application.Groups.Commands.AddGroupMember;

[Authorize(Permissions = Permission.GroupMember.Create, Policies = Policy.SelfOrAdmin)]
public record AddGroupMemberCommand(
    Ulid GroupId,
    HashSet<Ulid> UserIds
) : IAuthorizeableRequest<ErrorOr<Success>>
{
    public Ulid AuthorizedUserId { get; set; }
}