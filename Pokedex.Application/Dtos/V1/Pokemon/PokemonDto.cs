using Pokedex.Application.Dtos.V1.PokemonTipo;

namespace Pokedex.Application.Dtos.V1.Pokemon;

public class PokemonDto
{
    public int Id { get; set; } 
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public string? Imagem { get; set; }
    public PokemonTipoDto PokemonTipo { get; set; } = null!;
}