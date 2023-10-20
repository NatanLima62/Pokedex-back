namespace Pokedex.Application.Dtos.V1.Pokemon;

public class AtualizarPokemonDto
{
    public int Id { get; set; } 
    public string Nome { get; set; } = null!;
    public string Descricao { get; set; } = null!;
    public int PokemonTipoId { get; set; }
}