using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Seci.Domain.Contracts;
using Seci.Domain.Contracts.Repositories;
using Seci.Domain.Enties;
using Seci.Infra.Data.Context;

namespace Seci.Infra.Data.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity, IAggegateRoot
{
    protected readonly ApplicationSeciDbContext Context;
    private readonly DbSet<T> _dbSet;
    private bool _isDisposed;

    protected Repository(ApplicationSeciDbContext context)
    {
        Context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(expression);
    }

    public async Task<bool> Any(Expression<Func<T, bool>> expression)
    {
        return await _dbSet.AnyAsync(expression);
    }
    
    public IUnitOfWork UnitOfWork => Context;
    
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
            // free managed resources
            Context.Dispose();
        }

        _isDisposed = true;
    }
    
    ~Repository()
    {
        Dispose(false);
    }
}