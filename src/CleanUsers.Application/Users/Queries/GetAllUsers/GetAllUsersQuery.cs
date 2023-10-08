using CleanUsers.Domain.Common.Models;
using CleanUsers.Domain.Users;
using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery(GetAllUsersOptions Options) : IRequest<ErrorOr<PaginatedList<User>>>;
