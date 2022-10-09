using FluentValidation;
using ValidationException = CleanArchitecture.Application.Exceptions.ValitationException;
using MediatR;

namespace CleanArchitecture.Application.Behaviours;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validator;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validator)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (_validator.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResult =  await Task
                .WhenAll(_validator.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResult.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }
        }

        return await next();
    }
}
