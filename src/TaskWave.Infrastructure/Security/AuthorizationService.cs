using ErrorOr;

using TaskWave.Application.Common.Interfaces;
using TaskWave.Application.Common.Security.Requests;
using TaskWave.Infrastructure.Security.CurrentUserProvider;

using TaskWave.Infrastructure.Security.PolicyEnforcer;

namespace TaskWave.Infrastructure.Security;

public class AuthorizationService(
    IPolicyEnforcer policyEnforcer,
    ICurrentUserProvider currentUserProvider
) : IAuthorizationService
{
    public ErrorOr<Success> AuthorizeCurrentUser<T>(
        IAuthorizeableRequest<T> request,
        List<string> requiredRoles,
        List<string> requiredPermissions,
        List<string> requiredPolicies)
    {
        CurrentUser currentUser = currentUserProvider.GetCurrentUser();

        if (requiredPermissions.Except(currentUser.Permissions).Any())
        {
            return Error.Unauthorized(description: "User is missing required permissions for taking this action");
        }

        if (requiredRoles.Except(currentUser.Roles).Any())
        {
            return Error.Unauthorized(description: "User is missing required roles for taking this action");
        }

        foreach (string policy in requiredPolicies)
        {
            ErrorOr<Success> authorizationAgainstPolicyResult = policyEnforcer.Authorize(request, currentUser, policy);

            if (authorizationAgainstPolicyResult.IsError)
            {
                return authorizationAgainstPolicyResult.Errors;
            }
        }

        return Result.Success;
    }
}