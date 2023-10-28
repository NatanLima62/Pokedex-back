using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;

namespace Pokedex.Infra.Repositories;

public class PokemonTipoRepository : Repository<PokemonTipo>, IPokemonTipoRepository
{
    public PokemonTipoRepository(ApplicationPokedexDbContext context) : base(context)
    {
    }

    public async Task<List<PokemonTipo>> ObterTodos()
    {
        return await Context.PokemonTipos.Include(pt => pt.Pokemons).AsNoTrackingWithIdentityResolution().ToListAsync();
    }
}