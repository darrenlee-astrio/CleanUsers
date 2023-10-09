using CleanUsers.Api.Common.Constants;
using CleanUsers.Api.Common.Controllers;
using CleanUsers.Api.Contracts.Common;
using CleanUsers.Api.Contracts.Users;
using CleanUsers.Application.Users.Commands.DeleteUser;
using CleanUsers.Application.Users.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using DomainUserType = CleanUsers.Domain.Users.UserType;

namespace CleanUsers.Api.Users;

public class UsersController : ApiController
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(ApiEndpoints.Users.Get)]
    [SwaggerOperation("Gets a user by id")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(UserResponse))]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var command = new GetUserQuery(id);
        var result = await _mediator.Send(command);

        return result.Match(
            user => Ok(user.ToResponse()),
            Problem);
    }

    [HttpGet(ApiEndpoints.Users.GetAll)]
    [SwaggerOperation("Gets all users with optional filtering, sorting and pagination")]
    [SwaggerResponse(StatusCodes.Status200OK, type: typeof(PaginatedResponse<UserResponse>))]
    public async Task<IActionResult> GetAll([FromQuery] GetAllUsersRequest request, CancellationToken cancellationToken = default)
    {
        DomainUserType? userType = null;
        if (request.UserType is not null && !DomainUserType.TryFromName(request.UserType.ToString(), out userType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid user type");
        }

        var command = request.ToCommand(userType);
        var result = await _mediator.Send(command);

        return result.Match(
            users => Ok(users.ToResponse()),
            Problem);
    }

    [HttpPost(ApiEndpoints.Users.Create)]
    [SwaggerOperation("Creates a user")]
    [SwaggerResponse(StatusCodes.Status201Created, type: typeof(UserResponse))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(ValidationProblemDetails))]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken = default)
    {
        if (!DomainUserType.TryFromName(request.UserType.ToString(), out var userType))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                detail: "Invalid user type");
        }

        var command = request.ToCommand(userType);
        var result = await _mediator.Send(command);

        return result.Match(
            user => CreatedAtAction(nameof(GetById),
                                    new { id = user.Id },
                                    result.Value.ToResponse()),
            Problem);
    }

    [HttpDelete(ApiEndpoints.Users.Delete)]
    [SwaggerOperation("Deletes a user by id")]
    [SwaggerResponse(StatusCodes.Status204NoContent)]
    [SwaggerResponse(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteUserCommand(id);

        var result = await _mediator.Send(command);
        return result.Match(
            _ => NoContent(),
            Problem);
    }
}
