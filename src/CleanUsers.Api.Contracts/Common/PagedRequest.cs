namespace CleanUsers.Api.Contracts.Common;

public record PagedRequest(
    int Page = 1,
    int PageSize = 10);