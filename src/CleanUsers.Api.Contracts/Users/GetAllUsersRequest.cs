using CleanUsers.Api.Contracts.Common;

namespace CleanUsers.Api.Contracts.Users;

public record GetAllUsersRequest(
    string? Username,
    string? Name,
    UserType? UserType,
    DateTime? DateJoined,
    string? SortBy) : PagedRequest, ISortableRequest;