using CleanUsers.Application.Abstractions;
using CleanUsers.Domain.Users;
using CleanUsers.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanUsers.Infrastructure.Users.Persistence;

public class UsersRepository : IUsersRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UsersRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> ExistsIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<bool> ExistsUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(x => x.Username == username, cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
    }

    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users
            .FindAsync(id);
    }

    public Task RemoveAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(user);

        return Task.CompletedTask;
    }

    public Task RemoveRangeAsync(List<User> users, CancellationToken cancellationToken = default)
    {
        _dbContext.RemoveRange(users);

        return Task.CompletedTask;
    }

    public Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        _dbContext.Update(user);

        return Task.CompletedTask;
    }
}
