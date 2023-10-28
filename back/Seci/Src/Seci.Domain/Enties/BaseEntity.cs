using Seci.Domain.Contracts;

namespace Seci.Domain.Enties;

public abstract class BaseEntity : IEntity
{
    public int Id { get; set; }
}