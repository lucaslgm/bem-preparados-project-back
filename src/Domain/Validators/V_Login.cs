using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_Login : AbstractValidator<E_Login>
  {
    public V_Login()
    {
      RuleFor(User => User.Usuario)
      .NotNull()
      .WithMessage("O nome de usuário não pode ser nulo")
      .Length(0, 10)
      .WithMessage("A ome de usuário deve ter no máximo 10 caracteres");

      RuleFor(User => User.Senha)
      .NotNull()
      .WithMessage("A senha não pode ser nula")
      .Length(0, 10)
      .WithMessage("A senha deve ter no máximo 10 caracteres");
    }
  }
}
