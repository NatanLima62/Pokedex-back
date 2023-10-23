namespace Pokedex.Application.Dtos.V1.Pokemon;

public class AdicionarPokemonDto
{
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public int PokemonTipoId { get; set; }
    public string? Imagem { get; set; }
}   