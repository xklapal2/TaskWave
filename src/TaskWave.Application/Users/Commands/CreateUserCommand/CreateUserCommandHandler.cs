
using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Application.Users.Common;
using TaskWave.Domain.Entities;

namespace TaskWave.Application.Users.Commands.CreateUserCommand;

public class CreateUserCommandHandler(IUserRepository usersRepository) : IRequestHandler<CreateUserCommand, ErrorOr<UserResult>>
{
    public async Task<ErrorOr<UserResult>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await usersRepository.ExistsAsync(request.Email, cancellationToken))
        {
            return Error.Conflict(description: "An account with this email address already exists.");
        }

        User user = User.Create(request.FirstName, request.LastName, request.Email, request.Password);

        await usersRepository.AddAsync(user, cancellationToken);

        return UserResult.FromUser(user);
    }
}