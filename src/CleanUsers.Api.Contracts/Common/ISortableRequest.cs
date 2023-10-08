namespace CleanUsers.Api.Contracts.Common;

public interface ISortableRequest
{
    string? SortBy { get; init; }
}
