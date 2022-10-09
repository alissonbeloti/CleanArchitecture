using MediatR;

namespace CleanArchitecture.Application.Features.Director.Commands.CreateDirector;

public class CreateDirectorCommand: IRequest<int>
{
    public string Nome { get; set; } = String.Empty;
    public string Sobrenome { get; set; } = String.Empty;
    public int VideoId { get; set; }
}
