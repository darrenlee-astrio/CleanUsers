using FluentValidation;

namespace CleanUsers.Application.Users.Queries.GetAllUsers;

public class GetAllUsersOptionsValidator : AbstractValidator<GetAllUsersOptions>
{
    private static readonly string[] AcceptableSortFields =
    {
        "DateJoined"
    };

    public GetAllUsersOptionsValidator()
    {
        RuleFor(x => x.SortField)
            .Must(x => x is null || AcceptableSortFields.Contains(x, StringComparer.OrdinalIgnoreCase))
            .WithMessage("You can only sort by 'DateJoined'");
    }
}
