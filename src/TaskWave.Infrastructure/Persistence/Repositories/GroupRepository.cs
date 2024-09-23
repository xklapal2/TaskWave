using Microsoft.EntityFrameworkCore;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Application.Groups.Queries.GroupDetail;
using TaskWave.Domain.Entities.Groups;

namespace TaskWave.Infrastructure.Persistence.Repositories;

public class GroupRepository(AppDbContext dbContext) : IGroupRepository
{

    // #######################################
    // #####          QUERIES            #####
    // #######################################
    public async Task<bool> ExistsAsync(string groupName, CancellationToken cancellationToken)
    {
        return await dbContext.Groups
                .AsNoTracking()
                .AnyAsync(x => x.Name == groupName, cancellationToken);
    }

    public async Task<Group> GetByIdAsync(Ulid groupId, CancellationToken cancellationToken)
    {
        return await dbContext.Groups
            .Include(g => g.Members)
            .FirstAsync(g => g.Id == groupId, cancellationToken);
    }

    public Task<List<Group>> ListGroupsAsync(CancellationToken cancellationToken)
    {
        return dbContext.Groups.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<GroupDetail?> GroupDetailAsync(Ulid id, CancellationToken cancellationToken)
    {

        string sqlQuery = @"
            SELECT
                g.Id,
                g.Name,
                u.Id uid,
                u.FirstName,
                u.LastName,
                u.Email,
                gm.JoinedDate
            FROM Groups g
            LEFT JOIN GroupMembers gm ON g.Id = gm.GroupId
            LEFT JOIN Users u ON gm.UserId = u.Id
            WHERE g.Id = {0}";

        List<GroupMemberDTO> result = await dbContext
                        .Database
                        .SqlQueryRaw<GroupMemberDTO>(sqlQuery, id.ToString())
                        .ToListAsync(cancellationToken);

        if (result.FirstOrDefault() is not GroupMemberDTO first)
        {
            return null;
        }

        return new GroupDetail(
            first.Id,
            first.Name,
            result.ConvertAll(x =>
                new GroupMemberDetail
                (
                    x.Uid,
                    x.FirstName,
                    x.LastName,
                    x.Email
                )
            )
        );
    }

    private class GroupMemberDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Uid { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime JoinedDate { get; set; }
    };

    // #######################################
    // #####          COMMANDS           #####
    // #######################################
    public async Task AddAsync(Group group, CancellationToken cancellationToken)
    {
        await dbContext.Groups.AddAsync(group, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Group group, CancellationToken cancellationToken)
    {
        dbContext.Groups.Remove(group);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Group group, CancellationToken cancellationToken)
    {
        dbContext.Groups.Update(group);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}