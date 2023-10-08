using CleanUsers.Domain.Users;
using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid UserId) : IRequest<ErrorOr<User>>;
