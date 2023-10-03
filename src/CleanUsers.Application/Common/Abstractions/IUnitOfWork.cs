namespace CleanUsers.Application.Common.Abstractions;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
