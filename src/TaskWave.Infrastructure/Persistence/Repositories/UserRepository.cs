
using Microsoft.EntityFrameworkCore;

using TaskWave.Application.Common.Interfaces.Repositories;
using TaskWave.Domain.Entities;

namespace TaskWave.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        _ = await dbContext.AddAsync(user, cancellationToken);
        _ = await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.AsNoTracking().AnyAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email), cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Ulid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FindAsync(userId, cancellationToken);
    }
}