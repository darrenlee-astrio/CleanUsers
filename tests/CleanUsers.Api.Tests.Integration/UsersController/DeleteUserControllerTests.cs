using CleanUsers.Api.Contracts.Users;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CleanUsers.Api.Tests.Integration.UsersController;

public class DeleteUserControllerTests : IClassFixture<CleanUsersApiFactory>
{
    private readonly HttpClient _client;

    public DeleteUserControllerTests(CleanUsersApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task DeleteById_ReturnsNoContent_WhenUserExists()
    {
        // Arrange
        var createUserRequest = UsersControllerGenerator.CreateUserRequest();
        var createdResponse = await _client.PostAsJsonAsync(UsersApiEndpoints.Create, createUserRequest);
        var createdUser = await createdResponse.Content.ReadFromJsonAsync<UserResponse>();

        // Act
        var response = await _client.DeleteAsync(UsersApiEndpoints.DeleteById(createdUser!.Id));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task GetUserById_ReturnsNotFound_WhenUserDoesNotExist()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var response = await _client.DeleteAsync(UsersApiEndpoints.DeleteById(id));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
