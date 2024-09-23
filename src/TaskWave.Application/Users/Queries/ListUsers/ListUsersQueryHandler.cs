using ErrorOr;

using MediatR;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Domain.Entities;

namespace TaskWave.Application.Users.Queries.ListUsers;

public class ListUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<ListUsersQuery, ErrorOr<List<User>>>
{
    public async Task<ErrorOr<List<User>>> Handle(ListUsersQuery request, CancellationToken cancellationToken)
    {
        return await userRepository.ListUsersAsync(cancellationToken);
    }
}