using CleanUsers.Api.Contracts.Common;
using CleanUsers.Api.Contracts.Users;
using CleanUsers.Application.Common.Enums;
using CleanUsers.Application.Users.Commands.CreateUser;
using CleanUsers.Application.Users.Queries.GetAllUsers;
using CleanUsers.Domain.Common.Models;
using ContractUserType = CleanUsers.Api.Contracts.Users.UserType;
using DomainUser = CleanUsers.Domain.Users.User;
using DomainUserType = CleanUsers.Domain.Users.UserType;

namespace CleanUsers.Api.Users;

public static class UsersContractMapping
{
    public static CreateUserCommand ToCommand(this CreateUserRequest request, DomainUserType userType)
    {
        return new CreateUserCommand(
            Username: request.Username,
            Name: request.Name,
            Email: request.Email,
            Phone: request.Phone,
            UserType: userType);
    }

    public static GetAllUsersQuery ToCommand(this GetAllUsersRequest request, DomainUserType? userType = null)
    {
        return new GetAllUsersQuery(new GetAllUsersOptions
        {
            Username = request.Username,
            Name = request.Name,
            UserType = userType,
            DateJoined = request.DateJoined,
            SortField = request.SortBy?.TrimStart('+', '-'),
            SortOrder = request.SortBy is null ? SortOrder.Unsorted :
                request.SortBy.StartsWith('-') ? SortOrder.Descending : SortOrder.Ascending,
            Page = request.Page,
            PageSize = request.PageSize
        });
    }

    public static UserResponse ToResponse(this DomainUser user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Name = user.Name,
            Email = user.Email,
            Phone = user.Phone,
            UserType = user.UserType.ToResponse(),
            DateJoined = user.DateJoined
        };
    }

    public static PaginatedResponse<UserResponse> ToResponse(this PaginatedList<DomainUser> users)
    {
        return new PaginatedResponse<UserResponse>
        {
            PageIndex = users.PageIndex,
            TotalPages = users.TotalPages,
            Items = users.Items.ConvertAll(user => user.ToResponse())
        };
    }

    private static ContractUserType ToResponse(this DomainUserType userType)
    {
        return userType.Name switch
        {
            nameof(DomainUserType.Bronze) => ContractUserType.Bronze,
            nameof(DomainUserType.Silver) => ContractUserType.Silver,
            nameof(DomainUserType.Gold) => ContractUserType.Gold,
            _ => throw new InvalidOperationException(),
        };
    }
}
