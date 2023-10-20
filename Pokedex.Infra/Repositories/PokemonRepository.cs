using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;

namespace Pokedex.Infra.Repositories;

public class PokemonRepository : Repository<Pokemon>, IPokemonRepository
{
    public PokemonRepository(ApplicationPokedexDbContext context) : base(context)
    {
    }

    public void Adicionar(Pokemon pokemon)
    {
        Context.Pokemons.Add(pokemon);
    }

    public void Atualizar(Pokemon pokemon)
    {
        Context.Pokemons.Update(pokemon);
    }

    public async Task<Pokemon?> ObterPorId(int id)
    {
        return await Context.Pokemons.FirstOrDefaultAsync(p => p!.Id == id);
    }

    public void Remover(Pokemon pokemon)
    {
        Context.Pokemons.Remove(pokemon);
    }
}