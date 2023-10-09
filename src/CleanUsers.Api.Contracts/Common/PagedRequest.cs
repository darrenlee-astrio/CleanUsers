namespace CleanUsers.Api.Contracts.Common;

public abstract class PagedRequest
{
    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}