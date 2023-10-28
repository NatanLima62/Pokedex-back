using FluentValidation.Results;
using Seci.Domain.Contracts;

namespace Seci.Domain.Enties;

public abstract class Entity : BaseEntity, ITracking
{
    public DateTime CriadoEm { get; set; }
    public DateTime AtualizadoEm { get; set; }
    public int CriadoPor { get; set; }
    public int AtualizadoPor { get; set; }

    public virtual bool Validate(out ValidationResult validationResult)
    {
        validationResult = new ValidationResult();
        return validationResult.IsValid;
    }
}