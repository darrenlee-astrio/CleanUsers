namespace CleanUsers.Api.Contracts.Common;

public class PaginatedResponse<T>
{
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public List<T> Items { get; init; } = new();
}
