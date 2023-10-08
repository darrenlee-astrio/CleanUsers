using Bogus;
using Bogus.Extensions;
using CleanUsers.Domain.Users;

namespace CleanUsers.Infrastructure.Common.Persistence;

public static class DatabaseInitializer
{
    private static readonly Faker<User> _userGenerator = new Faker<User>()
        .RuleFor(x => x.Id, Guid.NewGuid)
        .RuleFor(x => x.Username, faker => faker.Person.UserName.ClampLength(8, 20))
        .RuleFor(x => x.Name, faker => faker.Person.FullName.ClampLength(8, 20))
        .RuleFor(x => x.Email, faker => faker.Person.Email)
        .RuleFor(x => x.Phone, "12345678")
        .RuleFor(x => x.UserType, UserType.Bronze)
        .RuleFor(x => x.DateJoined, faker => faker.Person.DateOfBirth);

    public static void Initialize(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
        {
            return;
        }

        var users = _userGenerator.Generate(10);
        context.Users.AddRange(users);
        context.SaveChanges();
    }
}
