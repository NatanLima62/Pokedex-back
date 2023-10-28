using FluentValidation.Results;
using Seci.Domain.Contracts;
using Seci.Domain.Validators;

namespace Seci.Domain.Enties;

public class Administrador : Entity, IAggegateRoot, ISoftDelete
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
    public bool Ativo { get; set; }

    public override bool Validate(out ValidationResult validationResult)
    {
        validationResult = new AdministradorValidator().Validate(this);
        return validationResult.IsValid;
    }
}