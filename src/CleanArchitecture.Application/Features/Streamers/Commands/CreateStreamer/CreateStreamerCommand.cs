using MediatR;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommand : IRequest<int>
{
    public string Nome { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;
}
