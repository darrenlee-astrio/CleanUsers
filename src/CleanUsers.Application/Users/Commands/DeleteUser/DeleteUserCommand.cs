using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<ErrorOr<Deleted>>;