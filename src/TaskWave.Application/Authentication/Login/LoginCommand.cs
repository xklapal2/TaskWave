using ErrorOr;

using MediatR;

namespace TaskWave.Application.Authentication.Login;

public record LoginCommand(
    string Email,
    string Password
)
: IRequest<ErrorOr<LoginResult>>;
