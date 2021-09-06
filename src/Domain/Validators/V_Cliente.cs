using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class ClienteValidator : AbstractValidator<E_Cliente>
  {
    public ClienteValidator()
    {
      RuleFor(Cliente => Cliente)
      .NotNull()
      .WithMessage("O Cliente não pode ser nulo");

      RuleFor(Cliente => Cliente.Cpf)
      .NotEmpty()
      .WithMessage("O CPF não pode ser nulo")
      .Length(11)
      .WithMessage("O CPF deve conter 11 caracteres");

      RuleFor(Cliente => Cliente.Nome)
      .NotEmpty()
      .WithMessage("O nome não pode ser nulo")
      .Length(0, 60)
      .WithMessage("O nome deve ter no máximo 60 caracteres");

      RuleFor(Cliente => Cliente.Dt_nascimento)
      .NotNull()
      .WithMessage("A data de nascimento não pode ser nula.");

      RuleFor(Cliente => Cliente.Genero)
      .NotNull()
      .WithMessage("O gênero não pode ser nulo.");

      RuleFor(Cliente => Cliente.Vlr_salario)
      .NotNull()
      .WithMessage("O salário não pode ser nulo.")
      .ScalePrecision(2, 12)
      .WithMessage("O valor financiado deve conter 2 casas decimais");

      RuleFor(Cliente => Cliente.Logradouro)
      .NotEmpty()
      .WithMessage("O logradouro não pode ser nulo")
      .Length(0, 60)
      .WithMessage("O logradouro deve ter no máximo 60 caracteres");

      RuleFor(Cliente => Cliente.Numero_residencia)
      .NotEmpty()
      .WithMessage("O número não pode ser nulo")
      .Length(0, 10)
      .WithMessage("O número deve ter no máximo 10 caracteres");

      RuleFor(Cliente => Cliente.Bairro)
      .NotEmpty()
      .WithMessage("O bairro não pode ser nulo")
      .Length(0, 60)
      .WithMessage("O bairo deve ter no máximo 60 caracteres");

      RuleFor(Cliente => Cliente.Cidade)
      .NotEmpty()
      .WithMessage("A cidade não pode ser nulo")
      .Length(0, 60)
      .WithMessage("A cidade deve ter no máximo 60 caracteres");

      RuleFor(Cliente => Cliente.Cep)
      .NotEmpty()
      .WithMessage("O cep não pode ser nulo")
      .Length(8)
      .WithMessage("O cep deve ter conter 08 caracteres");
    }
  }
}
