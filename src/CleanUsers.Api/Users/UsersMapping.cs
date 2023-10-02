using CleanUsers.Api.Contracts.Users;
using DomainUser = CleanUsers.Domain.Users.User;
using DomainUserType = CleanUsers.Domain.Users.UserType;

namespace CleanUsers.Api.Users;

public static class UsersMapping
{
    public static UserResponse ToResponse(this DomainUser user)
    {
        return new UserResponse(
            Id: user.Id,
            Username: user.Username,
            Name: user.Name,
            Email: user.Email,
            Phone: user.Phone,
            UserType: user.UserType.ToResponse(),
            DateJoined: user.DateJoined);
    }

    private static UserType ToResponse(this DomainUserType userType)
    {
        return userType.Name switch
        {
            nameof(DomainUserType.Bronze) => UserType.Bronze,
            nameof(DomainUserType.Silver) => UserType.Silver,
            nameof(DomainUserType.Gold) => UserType.Gold,
            _ => throw new InvalidOperationException(),
        };
    }
}
