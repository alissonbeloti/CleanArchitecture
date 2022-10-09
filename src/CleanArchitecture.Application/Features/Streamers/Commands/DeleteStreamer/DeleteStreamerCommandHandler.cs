using AutoMapper;

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerCommandHandler : IRequestHandler<DeleteStreamerCommand>
{
    //private readonly IStreamerRepository _streamerRepository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteStreamerCommandHandler> _logger;

    public DeleteStreamerCommandHandler(IUnityOfWork unityOfWork, IMapper mapper, ILogger<DeleteStreamerCommandHandler> logger)
    {
        _unityOfWork = unityOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await _unityOfWork.StreamerRepository.GetByIdAsync(request.Id);
        if (streamerToDelete == null)
        {
            _logger.LogError($"{request.Id} streamer id não existe no sistema.");
            throw new NotFoundException(nameof(Streamers), request.Id);
        }

        _unityOfWork.StreamerRepository.DeleteEntity(streamerToDelete);
        await _unityOfWork.Complete();

        _logger.LogInformation($"O {request.Id} streamer foi eliminado com existo!");
        return Unit.Value;
    }
}
