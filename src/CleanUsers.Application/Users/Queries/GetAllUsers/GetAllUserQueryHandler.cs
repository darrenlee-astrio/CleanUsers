using CleanUsers.Application.Common.Abstractions;
using CleanUsers.Domain.Common.Models;
using CleanUsers.Domain.Users;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace CleanUsers.Application.Users.Queries.GetAllUsers;

public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQuery, ErrorOr<PaginatedList<User>>>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IValidator<GetAllUsersOptions> _validator;

    public GetAllUserQueryHandler(
        IUsersRepository usersRepository,
        IValidator<GetAllUsersOptions> validator)
    {
        _usersRepository = usersRepository;
        _validator = validator;
    }

    public async Task<ErrorOr<PaginatedList<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request.Options, cancellationToken);

        return await _usersRepository.GetAllAsync(request.Options, cancellationToken);
    }
}
