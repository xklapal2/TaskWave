using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Common.Interfaces.Repositories;

public interface IGroupRepository
{
    Task<Group> GetByIdAsync(Ulid groupId, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(string groupName, CancellationToken cancellationToken);
    Task AddAsync(Group group, CancellationToken cancellationToken);
    Task UpdateAsync(Group group, CancellationToken cancellationToken);
    Task RemoveAsync(Group group, CancellationToken cancellationToken);
}