using System.Reflection;

using TaskWave.Application.Common.Interfaces;
using TaskWave.Application.Common.Security.Requests;

using ErrorOr;

using MediatR;

namespace TaskWave.Application.Common.Behaviors;

public class AuthorizationBehavior<TRequest, TResponse>(IAuthorizationService authorizationService)
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IAuthorizeableRequest<TResponse>
            where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        List<AuthorizeAttribute> authorizationAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>().ToList();

        if (authorizationAttributes.Count == 0)
        {
            return await next();
        }

        List<string> requiredPermissions = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Permissions?.Split(',') ?? [])
            .ToList();

        List<string> requiredRoles = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Roles?.Split(',') ?? [])
            .ToList();

        List<string> requiredPolicies = authorizationAttributes
            .SelectMany(authorizationAttribute => authorizationAttribute.Policies?.Split(',') ?? [])
            .ToList();

        ErrorOr<Success> authorizationResult = authorizationService.AuthorizeCurrentUser(
            request,
            requiredRoles,
            requiredPermissions,
            requiredPolicies);

        return authorizationResult.IsError
            ? (dynamic)authorizationResult.Errors
            : await next();
    }
}