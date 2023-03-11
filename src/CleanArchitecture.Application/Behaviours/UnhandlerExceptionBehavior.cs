using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviours;

public class UnhandlerExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest: IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandlerExceptionBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    //public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    //{
        
    //}

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            _logger.LogError(ex, $"Application Request: Ocorreu um erro para o {requestName} {request}");
            throw;
        }
    }
}
