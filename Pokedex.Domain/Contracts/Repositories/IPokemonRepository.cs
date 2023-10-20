using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Contracts.Repositories;

public interface IPokemonRepository : IRepository<Pokemon>
{
    void Adicionar(Pokemon pokemon);
    void Atualizar(Pokemon pokemon);
    Task<Pokemon?> ObterPorId(int id);
    void Remover(Pokemon pokemon);
}