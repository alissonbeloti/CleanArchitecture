namespace CleanArchitecture.Api.Errors;

public class CodeErrorResponse
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }

    public CodeErrorResponse(int statusCode, string? message = null)
    {
        StatusCode = statusCode;
        Message = message ?? GetDefaultMessageStatusCode(statusCode);
    }

    private string GetDefaultMessageStatusCode(int statusCode)
    {
        return statusCode switch
        {
            400 => "O Request enviado possui erros.",
            401 => "Não tem autorização para esse recurso.",
            404 => "Recurso solicitado não encontrado",
            500 => "Ocorreu erro interno no servidor.",
            _ => string.Empty,
        };
    }
}
