using Pokedex.Application.Dtos.V1.Pokemon;

namespace Pokedex.Application.Contracts;

public interface IPokemonService
{
    Task<PokemonDto?> Adicionar(AdicionarPokemonDto dto);
    Task<PokemonDto?> Atualizar(int id, AtualizarPokemonDto dto);
    Task<PokemonDto?> ObterPorId(int id);
    Task<List<PokemonDto>> ObterTodos();
    Task<List<PokemonDto>> Buscar(string nome, int tipoId);
    Task Remover(int id);
}