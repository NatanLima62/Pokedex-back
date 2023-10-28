using System.Linq.Expressions;
using Seci.Domain.Enties;

namespace Seci.Domain.Contracts.Repositories;

public interface IRepository<T> : IDisposable where T : BaseEntity, IAggegateRoot
{
    IUnitOfWork UnitOfWork { get; }
    Task<T?> FirstOrDefault(Expression<Func<T, bool>> expression);
    Task<bool> Any(Expression<Func<T, bool>> expression);
}