using CleanUsers.Api.Common.Constants;
using CleanUsers.Api.Contracts.Users;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace CleanUsers.Api.Tests.Integration.UsersController;

public class CreateUsersControllerTests : IClassFixture<CleanUsersApiFactory>
{
    private readonly HttpClient _client;

    public CreateUsersControllerTests(CleanUsersApiFactory apiFactory)
    {
        _client = apiFactory.CreateClient();
    }

    [Fact]
    public async Task Create_CreatesUser_WhenDataIsValid()
    {
        // Arrange
        var request = UsersControllerGenerator.CreateUserRequest();

        // Act
        var response = await _client.PostAsJsonAsync(ApiEndpoints.Users.Create, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();
        response.Headers.Location!.ToString().Should().Be($"http://localhost/{ApiEndpoints.Users.Create}/{userResponse!.Id}");
        userResponse.Should().BeEquivalentTo(request, options => options.ExcludingMissingMembers());
    }

    [Theory]
    [InlineData("12345_1")] // 7 characters
    [InlineData("12345_12345_12345_12345")] // 23 characters
    public async Task Create_ReturnsValidationError_WhenUsernameIsInvalid(string username)
    {
        // Arrange
        var request = UsersControllerGenerator.CreateUserRequestGenerator
            .RuleFor(x => x.Username, username).Generate();

        // Act
        var response = await _client.PostAsJsonAsync(ApiEndpoints.Users.Create, request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();
        error!.Status.Should().Be((int)HttpStatusCode.BadRequest);
        error.Title.Should().Be("One or more validation errors occurred.");
        error.Errors["Username"][0].Should().Be("The length of the username must be between 8 to 20 characters.");
    }
}
