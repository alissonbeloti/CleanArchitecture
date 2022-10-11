using AutoMapper;

using CleanArchitecture.Application.Contracts.Infrastructure;
using CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;

using Microsoft.Extensions.Logging;

using Moq;

using Shouldly;

using Xunit;

namespace CleanArchitecture.Application.UnitTests.Feature.Streamers.CreateStreamer;

public class CreateStreamerCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;
    private readonly Mock<IEmailService> _emailService;
    private readonly Mock<ILogger<CreateStreamerCommandHandler>> _logger;

    public CreateStreamerCommandHandlerTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(mapperConfig);
        _emailService = new Mock<IEmailService>();
        _logger = new Mock<ILogger<CreateStreamerCommandHandler>>();

        MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task CreateStreamerCommand_InputStreamer_ReturnsNumber()
    {
        var streamerInput = new CreateStreamerCommand
        {
            Nome = "Alisson Streaming",
            Url = "https://alissonstreaming.com.br"
        };

        var handler = new CreateStreamerCommandHandler(_mapper,
            _emailService.Object, _logger.Object, _unitOfWork.Object);

        var result = await handler.Handle(streamerInput, CancellationToken.None);

        result.ShouldBeOfType<int>();
    }
}
