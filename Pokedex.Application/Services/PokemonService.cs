using AutoMapper;
using Pokedex.Application.Contracts;
using Pokedex.Application.Dtos.V1.Base;
using Pokedex.Application.Dtos.V1.Pokemon;
using Pokedex.Application.Notifications;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Domain.Entities;

namespace Pokedex.Application.Services;

public class PokemonService : BaseService, IPokemonService
{
    private readonly IPokemonRepository _pokemonRepository;
    public PokemonService(IMapper mapper, INotificator notificator, IPokemonRepository pokemonRepository) : base(mapper, notificator)
    {
        _pokemonRepository = pokemonRepository;
    }

    public async Task<PokemonDto?> Adicionar(AdicionarPokemonDto dto)
    {
        var pokemon = Mapper.Map<Pokemon>(dto);
        if (!await Validar(pokemon))
        {
            return null;
        }
        
        _pokemonRepository.Adicionar(pokemon);
        if (await _pokemonRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<PokemonDto>(pokemon);
        }
        
        Notificator.Handle("Não foi possível cadastrar o pokemón!");
        return null;
    }

    public async Task<PokemonDto?> Atualizar(int id, AtualizarPokemonDto dto)
    {
        if (id != dto.Id)
        {
            Notificator.Handle("Os ids não conferem!");
            return null;
        }
        
        var pokemon = await _pokemonRepository.ObterPorId(id);
        if (pokemon == null)
        {
            Notificator.HandleNotFoundResourse();
            return null;
        }

        Mapper.Map(dto, pokemon);
        if (!await Validar(pokemon))
        {
            return null;
        }
        
        _pokemonRepository.Atualizar(pokemon);
        if (await _pokemonRepository.UnitOfWork.Commit())
        {
            return Mapper.Map<PokemonDto>(pokemon);
        }
        
        Notificator.Handle("Não foi possível atualizar o pokemón!");
        return null;
    }

    public async Task<PokemonDto?> ObterPorId(int id)
    {
        var pokemon = await _pokemonRepository.ObterPorId(id);
        if (pokemon != null)
            return Mapper.Map<PokemonDto>(pokemon);

        Notificator.HandleNotFoundResourse();
        return null;
    }

    public async Task<PagedDto<PokemonDto>> Buscar(BuscarPokemonDto dto)
    {
        var pokemon = await _pokemonRepository.Buscar(dto);
        return Mapper.Map<PagedDto<PokemonDto>>(pokemon);
    }

    public async Task Remover(int id)
    {
        var pokemon = await _pokemonRepository.ObterPorId(id);
        if (pokemon == null)
        {
            Notificator.HandleNotFoundResourse();
            return;
        }

        _pokemonRepository.Remover(pokemon);
        if (await _pokemonRepository.UnitOfWork.Commit())
        {
            return;
        }
        
        Notificator.Handle("Não foi possível remover o pokemón");
    }

    private async Task<bool> Validar(Pokemon pokemon)
    {
        if (!pokemon.Validar(out var validationResult))
        {
            Notificator.Handle(validationResult.Errors);
        }
        
        var existente = await _pokemonRepository.FirstOrDefault(u => u.Nome == pokemon.Nome && u.Id != pokemon.Id);
        if (existente != null)
        {
            Notificator.Handle(
                $"Já existe um pokemón com esse nome cadastrado.");
        }

        return !Notificator.HasNotification;
    }
}