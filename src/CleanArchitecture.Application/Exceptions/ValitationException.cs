using FluentValidation.Results;

namespace CleanArchitecture.Application.Exceptions;

public class ValitationException: ApplicationException
{

    public ValitationException(): base("Um ou mais erros de validação: ")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValitationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, falureGroup => falureGroup.ToArray());
    }

    public IDictionary<string, string[]> Errors { get; }

    
}
