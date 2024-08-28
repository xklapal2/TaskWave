using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Http;

using Throw;

namespace TaskWave.Infrastructure.Security.CurrentUserProvider;

public class CurrentUserProvider(IHttpContextAccessor httpContextAccessor) : ICurrentUserProvider
{
    public CurrentUser GetCurrentUser()
    {
        httpContextAccessor.HttpContext.ThrowIfNull();

        Ulid id = Ulid.Parse(GetSingleClaimValue("id"));
        string firstName = GetSingleClaimValue(JwtRegisteredClaimNames.Name);
        string lastName = GetSingleClaimValue(ClaimTypes.Surname);
        string email = GetSingleClaimValue(ClaimTypes.Email);
        List<string> permissions = GetClaimValues("permissions");
        List<string> roles = GetClaimValues(ClaimTypes.Role);

        return new CurrentUser(id, firstName, lastName, email, permissions, roles);
    }

    private List<string> GetClaimValues(string claimType) =>
        httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == claimType)
            .Select(claim => claim.Value)
            .ToList();

    private string GetSingleClaimValue(string claimType) =>
        httpContextAccessor.HttpContext!.User.Claims
            .Single(claim => claim.Type == claimType)
            .Value;
}