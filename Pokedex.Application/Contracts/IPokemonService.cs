using Pokedex.Application.Dtos.V1.Base;
using Pokedex.Application.Dtos.V1.Pokemon;

namespace Pokedex.Application.Contracts;

public interface IPokemonService
{
    Task<PokemonDto?> Adicionar(AdicionarPokemonDto dto);
    Task<PokemonDto?> Atualizar(int id, AtualizarPokemonDto dto);
    Task<PokemonDto?> ObterPorId(int id);
    Task<PagedDto<PokemonDto>> Buscar(BuscarPokemonDto dto);
    Task Remover(int id);
}