using Pokedex.Domain.Contracts;

namespace Pokedex.Domain.Entities;

public class BaseEntity : IEntity
{
    public int Id { get; set; }
}