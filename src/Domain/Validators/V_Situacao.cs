using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_Situacao : AbstractValidator<E_Situacao>
  {
    public V_Situacao()
    {
      RuleFor(State => State.situacao)
            .NotEmpty()
            .WithMessage("A situaçao não pode ser nula")
            .Length(2)
            .WithMessage("A situacao deve ter 2 caracteres");

      RuleFor(State => State.descricao)
            .NotEmpty()
            .WithMessage("A descrição não pode ser nulo")
            .Length(0, 25)
            .WithMessage("A descrição deve ter no máximo 25 caracteres");
    }
  }
}
