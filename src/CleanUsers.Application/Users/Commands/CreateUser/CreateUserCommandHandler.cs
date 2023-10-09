using CleanUsers.Application.Common.Abstractions;
using CleanUsers.Domain.Users;
using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ErrorOr<User>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<User>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var user = new User(
            username: command.Username,
            name: command.Name,
            email: command.Email,
            phone: command.Phone,
            userType: command.UserType);

        await _usersRepository.AddAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return user;
    }
}
