using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Pokedex.Domain.Contracts;
using Pokedex.Domain.Contracts.Paginacao;
using Pokedex.Domain.Contracts.Repositories;
using Pokedex.Domain.Entities;
using Pokedex.Infra.Contexts;
using Pokedex.Infra.Extensions;

namespace Pokedex.Infra.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity, IAggregateRoot
{
    private readonly DbSet<T> _dbSet;
    protected readonly ApplicationPokedexDbContext Context;
    private bool _isDisposed;
    public IUnitOfWork UnitOfWork => Context;

    protected Repository(ApplicationPokedexDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(expression).FirstOrDefaultAsync();
    }

    public async Task<bool> Any(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().Where(expression).AnyAsync();
    }

    public async Task<IResultadoPaginado<T>> Buscar(IQueryable<T> queryable, IBuscaPaginada<T> filtro)
    {
        filtro.AplicarFiltro(ref queryable);
        filtro.AplicarOrdenacao(ref queryable);
        
        return await queryable.BuscarPaginadoAsync(filtro.Pagina, filtro.TamanhoPagina);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed) return;

        if (disposing)
        {
            Context.Dispose();
        }

        _isDisposed = true;
    }
    
    ~Repository()
    {
        Dispose(false);
    }
}