using ErrorOr;

using TaskWave.Application.Common.Security.Policies;

using TaskWave.Application.Common.Security.Requests;
using TaskWave.Application.Common.Security.Roles;

using TaskWave.Infrastructure.Security.CurrentUserProvider;

namespace TaskWave.Infrastructure.Security.PolicyEnforcer;

public class PolicyEnforcer : IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser, string policy)
    {
        return policy switch
        {
            Policy.SelfOrAdmin => SelfOrAdminPolicy(request, currentUser),
            _ => Error.Unexpected(description: "Unknown policy name"),
        };
    }

    private static ErrorOr<Success> SelfOrAdminPolicy<T>(IAuthorizeableRequest<T> request, CurrentUser currentUser)
    {
        if (request.UserId == currentUser.Id || currentUser.Roles.Contains(Role.Admin))
        {
            return Result.Success;
        }
        else
        {
            return Error.Unauthorized(description: "Requesting user failed policy requirement");
        }
    }
}