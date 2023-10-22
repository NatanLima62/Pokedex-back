using Pokedex.Application.Dtos.V1.Pokemon;

namespace Pokedex.Application.Contracts;

public interface IPokemonService
{
    Task<PokemonDto?> Adicionar(AdicionarPokemonDto dto);
    Task<PokemonDto?> Atualizar(int id, AtualizarPokemonDto dto);
    Task<PokemonDto?> ObterPorId(int id);
    Task<PokemonDto?> ObterPorNome(string nome);
    Task<List<PokemonDto>> Buscar();
    Task<List<PokemonDto>> BuscarPorTipo(int tipoId);
    Task Remover(int id);
}