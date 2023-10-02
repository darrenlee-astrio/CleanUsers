using CleanUsers.Application.Abstractions;
using CleanUsers.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CleanUsers.Infrastructure.Common.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public DbSet<User> Users { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
