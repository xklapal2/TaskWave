using ErrorOr;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

namespace TaskWave.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IErrorOr
{
    private readonly IValidator<TRequest>? _validator = validator;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validator is null)
        {
            return await next();
        }

        ValidationResult validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid)
        {
            return await next();
        }

        List<Error> errors = validationResult.Errors.ConvertAll(
            error => Error.Validation(
                code: error.PropertyName,
                description: error.ErrorMessage
            )
        );

        return (dynamic)errors;
    }
}