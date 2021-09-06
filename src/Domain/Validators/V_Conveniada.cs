using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_Conveniada : AbstractValidator<E_Conveniada>
  {
    public V_Conveniada()
    {
      RuleFor(Affiliated => Affiliated.conveniada)
            .Length(6)
            .WithMessage("A conveniada deve possuir 06 caracteres");

      RuleFor(Affiliated => Affiliated.descricao)
            .NotEmpty()
            .WithMessage("A descrição não pode ser nulo")
            .Length(0, 25)
            .WithMessage("A descrição deve ter no máximo 20 caracteres");
    }
  }
}
