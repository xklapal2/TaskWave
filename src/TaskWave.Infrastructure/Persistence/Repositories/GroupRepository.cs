
using Microsoft.EntityFrameworkCore;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Infrastructure.Persistence.Repositories;

public class GroupRepository(AppDbContext dbContext) : IGroupRepository
{
    public async Task<Group> GetByIdAsync(Ulid groupId, CancellationToken cancellationToken)
    {
        return await dbContext.Groups
            .Include(g => g.Members)
            .FirstAsync(g => g.Id == groupId, cancellationToken);
    }

    public async Task AddAsync(Group group, CancellationToken cancellationToken)
    {
        await dbContext.Groups.AddAsync(group, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Group group, CancellationToken cancellationToken)
    {
        dbContext.Groups.Update(group);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Group group, CancellationToken cancellationToken)
    {
        dbContext.Groups.Remove(group);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string groupName, CancellationToken cancellationToken)
    {
        return await dbContext.Groups
                .AsNoTracking()
                .AnyAsync(x => x.Name == groupName, cancellationToken);
    }
}