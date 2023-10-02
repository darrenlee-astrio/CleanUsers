namespace CleanUsers.Domain.Users;

public class User
{
    public Guid Id { get; init; }
    public string Username { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string Email { get; init; } = null!;
    public string Phone { get; init; } = null!;
    public UserType UserType { get; init; } = null!;
    public DateTime DateJoined { get; init; }

    private User() { }

    public User(
        string username,
        string name,
        string email,
        string phone,
        UserType userType,
        Guid? id = null,
        DateTime? dateJoined = null)
    {

        Username = username;
        Name = name;
        Email = email;
        Phone = phone;
        UserType = userType;
        Id = id ?? Guid.NewGuid();
        DateJoined = dateJoined ?? DateTime.Now;
    }
}