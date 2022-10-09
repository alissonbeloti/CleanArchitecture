using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.CreateStreamer;

public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
{
    public CreateStreamerCommandValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("{Nome} não pode estar em branco")
            .NotNull()
            .MaximumLength(50).WithMessage("O Nome não pode exceder 50 caracteres.");

        RuleFor(p => p.Url)
            .NotEmpty().WithMessage("A {Url} não pode estar em branco");
    }
}
