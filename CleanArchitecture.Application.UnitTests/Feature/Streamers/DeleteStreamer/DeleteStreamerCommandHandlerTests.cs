using AutoMapper;

using CleanArchitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArchitecture.Application.Mappings;
using CleanArchitecture.Application.UnitTests.Mocks;
using CleanArchitecture.Infrastructure.Repositories;

using MediatR;

using Microsoft.Extensions.Logging;

using Moq;

using Shouldly;

using Xunit;

namespace CleanArchitecture.Application.UnitTests.Feature.Streamers.DeleteStreamer;

public class DeleteStreamerCommandHandlerTests
{
    private readonly IMapper _mapper;
    private readonly Mock<UnitOfWork> _unitOfWork;
    //private readonly Mock<IEmailService> _emailService;
    private readonly Mock<ILogger<DeleteStreamerCommandHandler>> _logger;

    public DeleteStreamerCommandHandlerTests()
    {
        _unitOfWork = MockUnitOfWork.GetUnitOfWork();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = new Mapper(mapperConfig);
        //_emailService = new Mock<IEmailService>();
        _logger = new Mock<ILogger<DeleteStreamerCommandHandler>>();

        MockStreamerRepository.AddDataStreamerRepository(_unitOfWork.Object.StreamerDbContext);
    }

    [Fact]
    public async Task DeleteStreamerCommand_InputStreamer_ReturnsUnit()
    {
        var streamerInput = new DeleteStreamerCommand
        {
            Id = 8001,
        };

        var handler = new DeleteStreamerCommandHandler(
            _unitOfWork.Object,
            _mapper,
            _logger.Object);

        var result = await handler.Handle(streamerInput, CancellationToken.None);

        result.ShouldBeOfType<Unit>();
    }
}
