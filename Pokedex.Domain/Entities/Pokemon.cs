using FluentValidation.Results;
using Pokedex.Core.Enums;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Validators;

namespace Pokedex.Domain.Entities;

public class Pokemon : Entity, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public EPokemonTipo Tipo { get; set; }

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new PokemonValidator().Validate(this);
        return validationResult.IsValid;
    }
}