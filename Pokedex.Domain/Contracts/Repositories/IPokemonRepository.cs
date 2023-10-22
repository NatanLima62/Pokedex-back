using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Contracts.Repositories;

public interface IPokemonRepository : IRepository<Pokemon>
{
    void Adicionar(Pokemon pokemon);
    void Atualizar(Pokemon pokemon);
    Task<Pokemon?> ObterPorId(int id);
    Task<List<Pokemon>> ObterTodos();
    Task<List<Pokemon>> Buscar(string nome, int tipoId);
    void Remover(Pokemon pokemon);
}