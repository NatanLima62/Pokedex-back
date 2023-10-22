using Pokedex.Domain.Contracts;

namespace Pokedex.Domain.Entities;

public class PokemonTipo : BaseEntity, IAggregateRoot
{
    public string Nome { get; set; } = null!;
    public List<Pokemon> Pokemons { get; set; } = new();
}