using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using TaskWave.Application.Common.Interfaces;

namespace TaskWave.Infrastructure.Security.TokenGenerator;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public string GenerateToken(Ulid id, string firstName, string lastName, string email, List<string> permissions, List<string> roles)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);

        List<Claim> claims =
        [
            new(JwtRegisteredClaimNames.Name, firstName),
            new(JwtRegisteredClaimNames.FamilyName, lastName),
            new(JwtRegisteredClaimNames.Email, email),
            new("id", id.ToString()),
        ];

        roles.ForEach(role => claims.Add(new(ClaimTypes.Role, role)));
        permissions.ForEach(permission => claims.Add(new("permissions", permission)));

        JwtSecurityToken token = new(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}