using CleanUsers.Api.Contracts.Users;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CleanUsers.Api.Tests.Integration.UsersController;

public class GetUserControllerTests : IClassFixture<CleanUsersApiFactory>
{
    private readonly CleanUsersApiFactory _apiFactory;
    private readonly HttpClient _client;

    private static readonly string GetByIdEndpoint = "api/users/{id:guid}";

    public GetUserControllerTests(CleanUsersApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task GetUserById_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var createUserRequest = UsersControllerGenerator.CreateUserRequest();
        var createdResponse = await _client.PostAsJsonAsync(UsersApiEndpoints.Create, createUserRequest);
        var createdUser = await createdResponse.Content.ReadFromJsonAsync<UserResponse>();

        // Act
        var response = await _client.GetAsync(UsersApiEndpoints.GetById(createdUser!.Id));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var retrievedUser = await response.Content.ReadFromJsonAsync<UserResponse>();
        retrievedUser.Should().BeEquivalentTo(createdUser);
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await _client.GetAsync(UsersApiEndpoints.GetById(id));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
