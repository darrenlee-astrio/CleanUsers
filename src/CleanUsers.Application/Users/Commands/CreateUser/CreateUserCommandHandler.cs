using CleanUsers.Application.Abstractions;
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

    public async Task<ErrorOr<User>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User(
            username: request.Username,
            name: request.Name,
            email: request.Email,
            phone: request.Phone,
            userType: request.UserType);

        await _usersRepository.AddAsync(user);
        await _unitOfWork.CommitChangesAsync();

        return user;
    }
}
