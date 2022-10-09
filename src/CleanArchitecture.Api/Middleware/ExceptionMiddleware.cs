using System.Net;
using System.Text.Json;

using CleanArchitecture.Api.Errors;
using CleanArchitecture.Application.Exceptions;

namespace CleanArchitecture.Api.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch  (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var result = string.Empty;

            var jsonOptions = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            switch (ex)
            {
                case NotFoundException notFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValitationException valitationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    var validationJson = JsonSerializer.Serialize(valitationException.Errors);
                    result = JsonSerializer.Serialize(new CodeErrorException(statusCode, ex.Message, validationJson), jsonOptions);
                    break;

                case BadRequestException badRequestException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    break;
            }
            if (string.IsNullOrEmpty(result))
            {
                result = JsonSerializer.Serialize(new CodeErrorException(statusCode, ex.Message, ex.StackTrace), jsonOptions);
            }
            
            context.Response.StatusCode = statusCode;   


            await context.Response.WriteAsync(result);
        }
    }
}
