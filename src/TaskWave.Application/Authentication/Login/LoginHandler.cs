using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces;
using TaskWave.Application.Common.Security.Permissions;
using TaskWave.Application.Common.Security.Roles;
using TaskWave.Domain.Entities;

namespace TaskWave.Application.Authentication.Login;

public class LoginQueryHandler : IRequestHandler<LoginCommand, ErrorOr<LoginResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<LoginResult>> Handle(LoginCommand query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByEmailAsync(query.Email, cancellationToken) is not User user)
        {
            return Error.Unauthorized(description: "Invalid email or password.");
        }

        if (false) // TODO: validate password
        {
            return Error.Unauthorized(description: "Invalid email or password.");
        }

        string token = _jwtTokenGenerator.GenerateToken(user.Id, user.FirstName, user.LastName, user.Email, [Permission.User.Create], [Role.Admin]);

        return new LoginResult(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            token
        );
    }
}
