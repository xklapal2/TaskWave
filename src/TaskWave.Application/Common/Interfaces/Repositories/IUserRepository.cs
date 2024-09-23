using TaskWave.Domain.Entities;

namespace TaskWave.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    // #######################################
    // #####          QUERIES            #####
    // #######################################
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken);
    Task<List<User>> ListUsersAsync(CancellationToken cancellationToken);

    // #######################################
    // #####          COMMANDS           #####
    // #######################################
    Task AddAsync(User user, CancellationToken cancellationToken);
}