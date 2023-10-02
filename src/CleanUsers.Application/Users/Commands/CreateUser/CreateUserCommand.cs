using CleanUsers.Domain.Users;
using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Name,
    string Email,
    string Phone,
    UserType UserType) : IRequest<ErrorOr<User>>;