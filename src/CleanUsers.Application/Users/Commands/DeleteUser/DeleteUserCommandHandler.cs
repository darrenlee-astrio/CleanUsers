using CleanUsers.Application.Common.Abstractions;
using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Deleted>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Deleted>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _usersRepository.GetByIdAsync(command.Id);

        if (user is null)
        {
            return Error.NotFound("User not found");
        }

        await _usersRepository.RemoveAsync(user, cancellationToken);
        await _unitOfWork.CommitChangesAsync();

        return Result.Deleted;
    }
}
