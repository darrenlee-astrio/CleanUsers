using Bogus;
using Bogus.Extensions;
using CleanUsers.Api.Contracts.Users;
using ContractUserType = CleanUsers.Api.Contracts.Users.UserType;

namespace CleanUsers.Api.Tests.Integration.UsersController;

public static class UsersControllerGenerator
{
    private static readonly Faker<CreateUserRequest> _createUserRequestGenerator = new Faker<CreateUserRequest>()
    .RuleFor(x => x.Username, faker => faker.Person.UserName.ClampLength(8, 20))
    .RuleFor(x => x.Name, faker => faker.Person.FullName.ClampLength(8, 20))
    .RuleFor(x => x.Email, faker => faker.Person.Email)
    .RuleFor(x => x.Phone, "12345678")
    .RuleFor(x => x.UserType, ContractUserType.Bronze);

    public static Faker<CreateUserRequest> CreateUserRequestGenerator { get { return _createUserRequestGenerator.Clone(); } }
    public static CreateUserRequest CreateUserRequest() => _createUserRequestGenerator.Generate();
    public static IEnumerable<CreateUserRequest> CreateUserRequests(int count) => _createUserRequestGenerator.Generate(count);
}
