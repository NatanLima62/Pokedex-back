using AutoMapper;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.V1.PokemonTipo;
using Pokedex.Application.Notifications;
using Pokedex.Domain.Contracts.Repositories;

namespace Pokedex.Application.Services;

public class PokemonTipoService : BaseService, IPokemonTipoService
{
    private readonly IPokemonTipoRepository _pokemonTipoRepository;
    public PokemonTipoService(IMapper mapper, INotificator notificator, IPokemonTipoRepository pokemonTipoRepository) : base(mapper, notificator)
    {
        _pokemonTipoRepository = pokemonTipoRepository;
    }

    public async Task<List<PokemonTipoDto>> ObterTodos()
    {
        var pokemonTipos = await _pokemonTipoRepository.ObterTodos();
        return Mapper.Map<List<PokemonTipoDto>>(pokemonTipos);
    }
}