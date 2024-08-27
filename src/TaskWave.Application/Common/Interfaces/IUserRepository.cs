using TaskWave.Domain.Entities;

namespace TaskWave.Application.Common.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken);
}