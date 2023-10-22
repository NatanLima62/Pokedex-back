using AutoMapper;
using Pokedex.Application.Dtos.V1.Pokemon;
using Pokedex.Application.Dtos.V1.PokemonTipo;
using Pokedex.Domain.Entities;

namespace Pokedex.Application.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PokemonDto, Pokemon>().ReverseMap();
        CreateMap<AdicionarPokemonDto, Pokemon>().ReverseMap();
        CreateMap<AtualizarPokemonDto, Pokemon>().ReverseMap();
        
        CreateMap<PokemonTipoDto, PokemonTipo>().ReverseMap();
    }
}