using CleanUsers.Application.Common.Abstractions;
using CleanUsers.Application.Users.Queries.GetAllUsers;
using CleanUsers.Domain.Users;
using CleanUsers.Infrastructure.Common.Models;
using CleanUsers.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using DomainModels = CleanUsers.Domain.Common.Models;

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

    public async Task<DomainModels.PaginatedList<User>> GetAllAsync(GetAllUsersOptions options, CancellationToken cancellationToken = default)
    {
        var users = _dbContext.Users.AsQueryable();

        users = options.Username is null ? users :
            users.Where(u => u.Username.Contains(options.Username));

        users = options.Name is null ? users :
            users.Where(u => u.Name.Contains(options.Name));

        users = options.UserType is null ? users :
            users.Where(u => u.UserType == options.UserType);

        users = options.DateJoined is null ? users :
            users.Where(u => u.DateJoined.Date == options.DateJoined.GetValueOrDefault().Date);

        var sortField = options.SortField is not null &&
            options.SortField.Equals("DateJoined", StringComparison.OrdinalIgnoreCase) ?
            "DateJoined" : null;

        users = sortField is null ? users :
            options.SortOrder == Application.Common.Enums.SortOrder.Descending ?
                users.OrderByDescending(u => EF.Property<object>(u, sortField)) :
                users.OrderBy(u => EF.Property<object>(u, sortField));

        var result = await PaginatedList<User>.CreateAsync(
            users.AsNoTracking(),
            options.Page,
            options.PageSize,
            cancellationToken);

        return new DomainModels.PaginatedList<User>(
            result, result.PageIndex, result.TotalPages);
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
