namespace CleanUsers.Api.Contracts.Users;

public record CreateUserRequest(
    string Username,
    string Name,
    string Email,
    string Phone,
    UserType UserType);
