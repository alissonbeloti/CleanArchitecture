using AutoMapper;

using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Application.Models;
using CleanArchitecture.Domain;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommandHandler : IRequestHandler<CreateStreamerCommand, int>
{
    //private readonly IStreamerRepository _streamerRepository;
    private readonly IUnityOfWork _unityOfWork;

    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    private readonly ILogger<CreateStreamerCommandHandler> _logger;

    public CreateStreamerCommandHandler(IMapper mapper, IEmailService emailService, ILogger<CreateStreamerCommandHandler> logger,
        IUnityOfWork unityOfWork)
    {
        //_streamerRepository = streamerRepository;
        _unityOfWork = unityOfWork;
        _mapper = mapper;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerEntity = _mapper.Map<Streamer>(request);

        //var newStreamer = await _streamerRepository.AddAsync(streamerEntity);
        _unityOfWork.StreamerRepository.AddEntity(streamerEntity);

        var result = await _unityOfWork.Complete();

        if (result <= 0)
        {
            throw new Exception($"Não foi possível inserir o streamer.");
        }

        _logger.LogInformation($"Streamer {streamerEntity.Id} created succesfull.");

        await SendEmail(streamerEntity);

        return streamerEntity.Id;
    }

    private async Task SendEmail(Streamer streamer)
    {
        var email = new Email
        {
            To = "alissonbeloti@yahoo.com.br",
            Body = "A empresa do streamer foi criada com exito",
            Subject = $"{streamer.Nome} foi criado com sucesso",
        };
        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Não enviou email de {streamer.Id} - {ex.Message}");
        }
    }
}
