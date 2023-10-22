using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Contracts.Repositories;

public interface IPokemonRepository : IRepository<Pokemon>
{
    void Adicionar(Pokemon pokemon);
    void Atualizar(Pokemon pokemon);
    Task<Pokemon?> ObterPorId(int id);
    Task<List<Pokemon>> ObterPorNome(string nome);
    Task<List<Pokemon>> ObterTodos();
    Task<List<Pokemon>> ObterPorTipo(int tipoId);
    void Remover(Pokemon pokemon);
}