namespace Seci.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}