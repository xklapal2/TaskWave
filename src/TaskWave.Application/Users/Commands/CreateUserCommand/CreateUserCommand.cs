
using ErrorOr;

using TaskWave.Application.Common.Security.Permissions;
using TaskWave.Application.Common.Security.Policies;
using TaskWave.Application.Common.Security.Requests;
using TaskWave.Application.Users.Common;

namespace TaskWave.Application.Users.Commands.CreateUserCommand;

[Authorize(Permissions = Permission.User.Get, Policies = Policy.SelfOrAdmin)]
public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IAuthorizeableRequest<ErrorOr<UserResult>>
{
    public Ulid AuthorizedUserId { get; set; }
}