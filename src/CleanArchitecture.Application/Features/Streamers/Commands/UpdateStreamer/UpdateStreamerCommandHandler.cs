using AutoMapper;

using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand>
{
    //private readonly IStreamerRepository _streamerRepository;
    private readonly IUnityOfWork _unityOfWork;

    private readonly IMapper _mapper;
    private readonly ILogger<UpdateStreamerCommandHandler> _logger;

    public UpdateStreamerCommandHandler(IUnityOfWork unityOfWork, IMapper mapper, ILogger<UpdateStreamerCommandHandler> logger)
    {
        _unityOfWork = unityOfWork;
        //_streamerRepository = streamerRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
    {
        //var streamer = await _streamerRepository.GetByIdAsync(request.Id);
        var streamer = await  _unityOfWork.StreamerRepository.GetByIdAsync(request.Id);

        if (streamer == null) 
        {
            _logger.LogError($"Não foi possível encontrar o streamerId {request.Id}");
            throw new NotFoundException(nameof(Streamer), request.Id); 
        }

        //passa o valor das propriedades do request ao Streamer que veio da base de dados,
        //Semp perder as demais informações
        _mapper.Map(request, streamer, typeof(UpdateStreamerCommand), typeof(Streamer));

        // _streamerRepository.UpdateAsync(streamer);
        _unityOfWork.StreamerRepository.UpdateEntity(streamer);
        await _unityOfWork.Complete();

        _logger.LogInformation($"A operação foi concluída com êxito para o streamerId {request.Id}");

        return Unit.Value;
    }
}
