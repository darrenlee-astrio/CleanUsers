namespace CleanUsers.Api.Contracts.Users;

public class CreateUserRequest
{
    public required string Username { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public UserType UserType { get; init; }

    public CreateUserRequest()
    {
    }
}