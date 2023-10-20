using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts.Paginacao;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Extensions;

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

    public void Remover(Pokemon pokemon)
    {
        Context.Pokemons.Remove(pokemon);
    }

    public override async Task<IResultadoPaginado<Pokemon>> Buscar(IBuscaPaginada<Pokemon> filtro)
    {
        var queryable = Context.Pokemons.Include(p => p.PokemonTipo).AsQueryable();
        
        filtro.AplicarFiltro(ref queryable);
        filtro.AplicarOrdenacao(ref queryable);
        
        return await queryable.BuscarPaginadoAsync(filtro.Pagina, filtro.TamanhoPagina);
    }
}