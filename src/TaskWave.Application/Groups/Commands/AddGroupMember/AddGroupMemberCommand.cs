using ErrorOr;

using TaskWave.Application.Common.Security.Requests;

namespace TaskWave.Application.Groups.Commands.AddGroupMember;

public record AddGroupMemberCommand(
    Ulid GroupId,
    Ulid UserId
) : IAuthorizeableRequest<ErrorOr<Success>>
{
    public Ulid AuthUserId { get; set; }
}