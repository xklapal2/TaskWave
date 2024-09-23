using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Application.Common.Interfaces.Repositories;

public interface IGroupRepository
{
    // #######################################
    // #####          QUERIES            #####
    // #######################################
    Task<bool> ExistsAsync(string groupName, CancellationToken cancellationToken);
    Task<Group> GetByIdAsync(Ulid groupId, CancellationToken cancellationToken);
    Task<List<Group>> ListGroupsAsync(CancellationToken cancellationToken);

    // #######################################
    // #####          COMMANDS           #####
    // #######################################
    Task AddAsync(Group group, CancellationToken cancellationToken);
    Task RemoveAsync(Group group, CancellationToken cancellationToken);
    Task UpdateAsync(Group group, CancellationToken cancellationToken);
}