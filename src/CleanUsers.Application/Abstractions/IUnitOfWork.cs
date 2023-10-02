namespace CleanUsers.Application.Abstractions;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
