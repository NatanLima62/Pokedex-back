using System.Linq.Expressions;
using Pokedex.Domain.Contracts.Paginacao;
using Pokedex.Domain.Entities;

namespace Pokedex.Domain.Contracts.Repositories;

public interface IRepository<T> : IDisposable where T : BaseEntity, IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
    Task<IResultadoPaginado<T>> Buscar(IBuscaPaginada<T> filtro);
    Task<IResultadoPaginado<T>> Buscar(IQueryable<T> queryable, IBuscaPaginada<T> filtro);
}