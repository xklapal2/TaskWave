using ErrorOr;

using MediatR;

using TaskWave.Application.Users.Common;

namespace TaskWave.Application.Users.Commands.CreateUserCommand;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<ErrorOr<UserResult>>;