using CleanUsers.Application.Common.Helpers;
using ErrorOr;
using FluentValidation;
using MediatR;

namespace CleanUsers.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator == null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        return ErrorHelper.TryCreateResponseFromErrors<TResponse>(validationResult.Errors.ToList(), out var response)
            ? response
            : throw new ValidationException(validationResult.Errors);
    }
}
