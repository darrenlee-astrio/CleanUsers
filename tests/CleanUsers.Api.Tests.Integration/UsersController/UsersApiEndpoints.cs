namespace CleanUsers.Api.Tests.Integration.UsersController;

public static class UsersApiEndpoints
{
    private const string Base = $"api/users";

    public const string Create = Base;
    public const string GetAll = Base;
    public static string GetById(Guid id) => $"{Base}/{id}";
    public static string DeleteById(Guid id) => $"{Base}/{id}";
}
