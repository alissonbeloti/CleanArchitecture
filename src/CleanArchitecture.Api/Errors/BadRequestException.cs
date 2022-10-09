namespace CleanArchitecture.Api.Errors;

public class BadRequestException: ApplicationException
{
	public BadRequestException(string message): base(message)
	{

	}
}
