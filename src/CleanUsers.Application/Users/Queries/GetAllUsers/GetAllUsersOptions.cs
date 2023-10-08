using CleanUsers.Application.Common.Enums;
using CleanUsers.Domain.Users;

namespace CleanUsers.Application.Users.Queries.GetAllUsers;

public class GetAllUsersOptions
{
    public string? Username { get; set; }
    public string? Name { get; set; }
    public UserType? UserType { get; set; }
    public DateTime? DateJoined { get; set; }

    public string? SortField { get; set; }
    public SortOrder? SortOrder { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }
}
