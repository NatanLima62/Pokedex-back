using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Contracts.Repositories;

public interface IPokemonTipoRepository : IRepository<PokemonTipo>
{
    Task<List<PokemonTipo>> ObterTodos();
}