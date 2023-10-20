namespace Pokedex.Domain.Entities;

public class PokemonTipo : BaseEntity
{
    public string Nome { get; set; } = null!;
    public List<Pokemon> Pokemons { get; set; } = new();
}