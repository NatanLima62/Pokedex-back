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
        return await Context.Pokemons.Include(p => p.PokemonTipo).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Pokemon>> ObterTodos()
    {
        return await Context.Pokemons.Include(p => p.PokemonTipo).ToListAsync();
    }

    public async Task<List<Pokemon>> Buscar(string nome, int tipoId)
    {
        var query = Context.Pokemons.Include(p => p.PokemonTipo)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nome))
        {
            query = query.Where(p => p.Nome.Contains(nome));
        }

        if (tipoId > 0)
        {
            query = query.Where(p => p.PokemonTipoId == tipoId);
        }
        
        return await query.ToListAsync();
    }

    public void Remover(Pokemon pokemon)
    {
        Context.Pokemons.Remove(pokemon);
    }
}