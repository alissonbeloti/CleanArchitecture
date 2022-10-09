using FluentValidation;

namespace CleanArchitecture.Application.Features.Director.Commands.CreateDirector;

internal class CreateCommandValidator: AbstractValidator<CreateDirectorCommand>
{
	public CreateCommandValidator()
	{
		RuleFor(p => p.Nome)
			.NotNull().WithMessage("{Nome} não pode ser nulo.");

        RuleFor(p => p.Sobrenome)
            .NotNull().WithMessage("{Sobrenome} não pode ser nulo.");
    }
}
