using CleanUsers.Application.Common.Abstractions;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanUsers.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly IUsersRepository _usersRepository;

    public CreateUserCommandValidator(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;

        RuleFor(x => x.Username)
             .NotEmpty().WithMessage("Username is required")
             .Length(8, 20).WithMessage("The length of the username must be between 8 to 20 characters.")
             .Must(BeAValidUsername).WithMessage("Username already exists");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .Length(8, 20).WithMessage("The length of the username must be between 8 to 20 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email address is required")
            .EmailAddress().WithMessage("A valid email address is required");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone number is required")
            .Length(8).WithMessage("Phone number can only contain 8 characters")
            .Must(BeAValidPhoneNumber).WithMessage("A valid phone number is required");

        RuleFor(x => x.UserType)
            .NotEmpty().WithMessage("User type is required");
    }

    private bool BeAValidUsername(string username)
    {
        var usernameExists = _usersRepository.ExistsUsernameAsync(username).Result;
        return usernameExists == false;
    }

    private bool BeAValidPhoneNumber(string phoneNumber)
    {
        // can only contain numerical character 0-9
        return new Regex("^[0-9]+$").IsMatch(phoneNumber);
    }
}
