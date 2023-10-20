using FluentValidation.Results;
using Pokedex.Domain.Contracts;

namespace Pokedex.Domain.Entities;

public class Entity : BaseEntity, ITracking
{
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    
    public virtual bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}