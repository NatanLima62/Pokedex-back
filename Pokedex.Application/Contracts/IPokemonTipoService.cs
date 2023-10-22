using Pokedex.Application.Dtos.V1.PokemonTipo;

namespace Pokedex.Application.Contracts;

public interface IPokemonTipoService
{
    Task<List<PokemonTipoDto>> ObterTodos();
}