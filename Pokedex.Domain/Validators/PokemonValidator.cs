using FluentValidation;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Validators;

public class PokemonValidator : AbstractValidator<Pokemon>
{
    public PokemonValidator()
    {
        RuleFor(p => p.Nome)
            .MaximumLength(120);

        RuleFor(p => p.Descricao)
            .MaximumLength(250);

        RuleFor(p => p.PokemonTipoId)
            .NotNull()
            .WithMessage("O tipo não pode ser nulo")
            
            .NotEmpty()
            .WithMessage("O tipo não pode ser vazio");
    }
}