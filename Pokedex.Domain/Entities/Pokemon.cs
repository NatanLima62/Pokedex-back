using FluentValidation.Results;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Validators;

namespace Pokedex.Domain.Entities;

public class Pokemon : Entity, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public int PokemonTipoId { get; set; }

    public PokemonTipo PokemonTipo { get; set; } = null!;

    public override bool Validar(out ValidationResult validationResult)
    {
        validationResult = new PokemonValidator().Validate(this);
        return validationResult.IsValid;
    }
}