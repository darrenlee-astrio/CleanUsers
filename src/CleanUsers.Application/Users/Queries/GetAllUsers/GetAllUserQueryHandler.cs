using CleanUsers.Application.Common.Abstractions;
using CleanUsers.Application.Common.Helpers;
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

    public async Task<ErrorOr<PaginatedList<User>>> Handle(GetAllUsersQuery command, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(command.Options, cancellationToken);
        var validationResult = await _validator.ValidateAsync(command.Options, cancellationToken);

        if (validationResult.IsValid)
        {
            return await _usersRepository.GetAllAsync(command.Options, cancellationToken);
        }
        return ErrorHelper.TryCreateResponseFromErrors<ErrorOr<PaginatedList<User>>>(validationResult.Errors.ToList(), out var response)
            ? response
            : throw new ValidationException(validationResult.Errors);
    }
}
