using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace CleanUsers.Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            _logger.LogError(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var problem = new ValidationProblemDetails(ToModelState(ex.Errors));
            problem.Status = (int)HttpStatusCode.BadRequest;
            problem.Type = "Validation exception";
            problem.Title = "One or more validation errors occurred.";

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problem);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, ex.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problem = new ProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError,
                Type = "Server error",
                Title = "Server error",
                Detail = "An internal server error has occurred."
            };

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(problem);
        }
    }

    protected ModelStateDictionary ToModelState(IEnumerable<ValidationFailure> errors)
    {
        var modelStateDictionary = new ModelStateDictionary();

        foreach (var error in errors)
        {
            modelStateDictionary.AddModelError(
                error.PropertyName,
                error.ErrorMessage);
        }

        return modelStateDictionary;
    }
}
