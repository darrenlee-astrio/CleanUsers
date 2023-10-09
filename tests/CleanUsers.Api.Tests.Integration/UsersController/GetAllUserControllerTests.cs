using CleanUsers.Api.Contracts.Common;
using CleanUsers.Api.Contracts.Users;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CleanUsers.Api.Tests.Integration.UsersController;

public class GetAllUserControllerTests : IClassFixture<CleanUsersApiFactory>
{
    private readonly HttpClient _client;
    public GetAllUserControllerTests(CleanUsersApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_ReturnsAllUsers_WhenUsersExists()
    {
        // Act
        var response = await _client.GetAsync(UsersApiEndpoints.GetAll);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var usersResponse = await response.Content.ReadFromJsonAsync<PaginatedResponse<UserResponse>>();
        usersResponse!.Items.Count.Should().Be(10);

    }
}
