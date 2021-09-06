using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_LimitesIdade : AbstractValidator<E_LimitesIdade>
  {
    public V_LimitesIdade()
    {
      RuleFor(LimitesIdade => LimitesIdade)
      .NotNull();

      RuleFor(LimitesIdade => LimitesIdade.Conveniada)
      .NotNull()
      .WithMessage("A conveniada não pode ser nula")
      .Length(0, 6)
      .WithMessage("A conveniada deve ter no máximo 06 caracteres");

      RuleFor(LimitesIdade => LimitesIdade.Idade_inicial)
      .NotNull()
      .WithMessage("A idade não pode ser nula");

      RuleFor(LimitesIdade => LimitesIdade.Idade_final)
      .NotNull()
      .WithMessage("A idade não pode ser nula");

      RuleFor(LimitesIdade => LimitesIdade.Valor_limite)
      .NotNull()
      .WithMessage("O valor não pode ser nulo")
      .ScalePrecision(2, 12);

      RuleFor(LimitesIdade => LimitesIdade.Percentual_maximo_analise)
      .NotNull()
      .WithMessage("O percentual não pode ser nulo");




    }
  }
}
