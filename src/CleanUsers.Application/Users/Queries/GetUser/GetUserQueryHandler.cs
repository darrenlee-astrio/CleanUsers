using CleanUsers.Application.Common.Abstractions;
using CleanUsers.Domain.Users;
using ErrorOr;
using MediatR;

namespace CleanUsers.Application.Users.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
    private readonly IUsersRepository _usersRepository;

    public GetUserQueryHandler(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<User>> Handle(GetUserQuery command, CancellationToken cancellationToken)
    {
        if (await _usersRepository.GetByIdAsync(command.UserId) is not User user)
        {
            return Error.NotFound(description: "User not found");
        }

        return user;
    }
}
