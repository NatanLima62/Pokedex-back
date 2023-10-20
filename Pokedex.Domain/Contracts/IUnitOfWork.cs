namespace Pokedex.Domain.Contracts;

public interface IUnitOfWork
{
    Task<bool> Commit();
}