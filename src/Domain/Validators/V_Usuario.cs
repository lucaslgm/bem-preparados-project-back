using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_Usuario : AbstractValidator<E_Usuario>
  {
    public V_Usuario()
    {
      RuleFor(user => user)
      .NotEmpty()
      .WithMessage("A entidade não pode ser vazia")
      .NotNull()
      .WithMessage("A entidade não pode ser nula");

      RuleFor(User => User.Usuario)
      .NotNull()
      .WithMessage("O nome de usuário não pode ser nulo")
      .Length(0, 10)
      .WithMessage("O nome de usuário deve ter no máximo 10 caracteres");

      RuleFor(User => User.Nome)
      .NotNull()
      .WithMessage("O nome não pode ser nulo")
      .Length(0, 20)
      .WithMessage("O nome deve ter no máximo 20 caracteres");

      RuleFor(User => User.Senha)
      .NotNull()
      .WithMessage("A senha não pode ser nula")
      .Length(0, 10)
      .WithMessage("A senha deve ter no máximo 10 caracteres");
    }
  }
}
