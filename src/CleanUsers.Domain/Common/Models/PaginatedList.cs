namespace CleanUsers.Domain.Common.Models;

public class PaginatedList<T> where T : class
{
    public int PageIndex { get; init; }
    public int TotalPages { get; init; }
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public List<T> Items { get; init; }

    public PaginatedList(List<T> items, int pageIndex, int totalPages)
    {
        PageIndex = pageIndex;
        TotalPages = totalPages;
        Items = items;
    }
}
