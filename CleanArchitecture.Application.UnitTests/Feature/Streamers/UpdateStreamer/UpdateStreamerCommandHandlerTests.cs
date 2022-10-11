using AutoMapper;

using CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using Shouldly;

using Xunit;

namespace CleanArchitecture.Application.UnitTests.Feature.Streamers.UpdateStreamer;

public class UpdateStreamerCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;
    //private readonly Mock<IEmailService> _emailService;
    private readonly Mock<ILogger<UpdateStreamerCommandHandler>> _logger;

    public UpdateStreamerCommandHandlerTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(mapperConfig);
        //_emailService = new Mock<IEmailService>();
        _logger = new Mock<ILogger<UpdateStreamerCommandHandler>>();

        MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task UpdateStreamerCommand_InputStreamer_ReturnsUnit()
    {
        var streamerInput = new UpdateStreamerCommand
        {
            Id = 8001,
            Nome = "Alisson Streaming Max",
            Url = "https://maximo.com.br"
        };

        var handler = new UpdateStreamerCommandHandler(
            _unitOfWork.Object,
            _mapper,
            _logger.Object);

        var result = await handler.Handle(streamerInput, CancellationToken.None);

        result.ShouldBeOfType<Unit>();
    }
}
