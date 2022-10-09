using MediatR;

using AutoMapper;

using Microsoft.Extensions.Logging;

using CleanArchitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Application.Features.Director.Commands.CreateDirector;

public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
{
    private readonly ILogger<CreateDirectorCommand> _logger;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unitOfWork;

    public CreateDirectorCommandHandler(ILogger<CreateDirectorCommand> logger, 
        IMapper mapper, 
        IUnityOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
    {
        var directorEntity = _mapper.Map<Domain.Director>(request);

        _unitOfWork.Repository<Domain.Director>().AddEntity(directorEntity);
        var result = await _unitOfWork.Complete();

        if (result <= 0)
        {
            _logger.LogError("Não foi possível inserir o Director.");
            throw new Exception("Não foi possível inserir o Director.");
        }

        return directorEntity.Id;
    }
}
