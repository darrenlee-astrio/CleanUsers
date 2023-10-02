namespace CleanUsers.Api.Contracts.Users;

public record UserResponse(
    Guid Id,
    string Username,
    string Name,
    string Email,
    string Phone,
    UserType UserType,
    DateTime DateJoined);