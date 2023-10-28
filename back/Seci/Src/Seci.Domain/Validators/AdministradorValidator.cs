using FluentValidation;
using Seci.Domain.Enties;

namespace Seci.Domain.Validators;

public class AdministradorValidator : AbstractValidator<Administrador>
{
    public AdministradorValidator()
    {
        RuleFor(a => a.Nome)
            .Length(3, 120)
            .WithMessage("O nome deve ter no mínimo 3 e no máximo 120 caracteres");

        RuleFor(a => a.Email)
            .EmailAddress();
        
        RuleFor(a => a.Senha)
            .Length(8, 40)
            .WithMessage("A senha deve ter no mínimo 3 e no máximo 120 caracteres");
    }
}