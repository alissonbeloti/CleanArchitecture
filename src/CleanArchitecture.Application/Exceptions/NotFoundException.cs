namespace CleanArchitecture.Application.Exceptions;

public class NotFoundException : ApplicationException
{
    public NotFoundException(string name, object context) : base($"Entity \"{name}\" ({context}) não foi encontrado ")
    {
    }
}
