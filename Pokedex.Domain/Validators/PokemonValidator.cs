using FluentValidation;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Validators;

public class PokemonValidator : AbstractValidator<Pokemon>
{
    public PokemonValidator()
    {
        RuleFor(p => p.Nome)
            .Length(3, 120)
            .WithMessage("O Nome deve ter no mínimo 3 e no máximo 120 caracteres")
            
            .NotNull()
            .WithMessage("O Nome não pode ser nulo")
            
            .NotEmpty()
            .WithMessage("O Nome não pode ser vazio");

        RuleFor(p => p.Descricao)
            .Length(3, 250)
            .WithMessage("O Descricao deve ter no mínimo 3 e no máximo 120 caracteres")
            
            .NotNull()
            .WithMessage("O Descricao não pode ser nula")
            
            .NotEmpty()
            .WithMessage("O Descricao pode ser vazia");

        RuleFor(p => p.PokemonTipoId)
            .NotNull()
            .WithMessage("O tipo não pode ser nulo")
            
            .NotEmpty()
            .WithMessage("O tipo não pode ser vazio");
    }
}