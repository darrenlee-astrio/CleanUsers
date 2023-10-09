namespace CleanUsers.Api.Contracts.Users;

public class UserResponse
{
    public Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Name { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required UserType UserType { get; init; }
    public DateTime DateJoined { get; init; }

    public UserResponse()
    {
    }
}