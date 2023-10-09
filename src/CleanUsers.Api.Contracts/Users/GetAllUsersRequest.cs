using CleanUsers.Api.Contracts.Common;

namespace CleanUsers.Api.Contracts.Users;

public class GetAllUsersRequest : PagedRequest, ISortableRequest
{
    public string? Username { get; init; }
    public string? Name { get; init; }
    public UserType? UserType { get; init; }
    public DateTime? DateJoined { get; init; }
    public string? SortBy { get; init; }

    public GetAllUsersRequest()
    {
    }
}