using ErrorOr;

using TaskWave.Application.Common.Security.Requests;

using TaskWave.Infrastructure.Security.CurrentUserProvider;

namespace TaskWave.Infrastructure.Security.PolicyEnforcer;

public interface IPolicyEnforcer
{
    public ErrorOr<Success> Authorize<T>(
        IAuthorizeableRequest<T> request,
        CurrentUser currentUser,
        string policy
    );
}