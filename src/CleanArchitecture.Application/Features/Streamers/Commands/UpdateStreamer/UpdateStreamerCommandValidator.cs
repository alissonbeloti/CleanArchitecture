using FluentValidation;

namespace CleanArchitecture.Application.Features.Streamers.Commands.UpdateStreamer;

public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
{
    public UpdateStreamerCommandValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("{Nome} não pode estar em branco")
            .NotNull()
            .MaximumLength(50).WithMessage("O Nome não pode exceder 50 caracteres.");

        RuleFor(p => p.Url)
            .NotNull().WithMessage("A {Url} não pode estar com valor null");
    }
}
