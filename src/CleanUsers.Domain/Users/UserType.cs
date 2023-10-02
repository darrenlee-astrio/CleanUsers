using Ardalis.SmartEnum;

namespace CleanUsers.Domain.Users;

public class UserType : SmartEnum<UserType>
{
    public static readonly UserType Bronze = new(nameof(Bronze), 0);
    public static readonly UserType Silver = new(nameof(Silver), 1);
    public static readonly UserType Gold = new(nameof(Gold), 2);
    public UserType(string name, int value)
        : base(name, value)
    {
    }
}
