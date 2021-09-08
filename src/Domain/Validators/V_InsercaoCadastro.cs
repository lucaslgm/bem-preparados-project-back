using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_InsercaoCadastro : AbstractValidator<E_InsercaoCadastro>
  {
    public V_InsercaoCadastro()
    {
      RuleFor(Cadastro => Cadastro)
    .NotNull()
    .WithMessage("O Cadastro não pode ser nulo");

      RuleFor(Cadastro => Cadastro.Cpf)
      .NotEmpty()
      .WithMessage("O CPF não pode ser nulo")
      .Length(11)
      .WithMessage("O CPF deve conter 11 caracteres");

      RuleFor(Cadastro => Cadastro.Nome)
      .NotEmpty()
      .WithMessage("O nome não pode ser nulo")
      .Length(0, 60)
      .WithMessage("O nome deve ter no máximo 60 caracteres");

      RuleFor(Cadastro => Cadastro.Dt_nascimento)
      .NotNull()
      .WithMessage("A data de nascimento não pode ser nula.");

      RuleFor(Cadastro => Cadastro.Genero)
      .NotNull()
      .WithMessage("O gênero não pode ser nulo.");

      RuleFor(Cadastro => Cadastro.Vlr_salario)
      .NotNull()
      .WithMessage("O salário não pode ser nulo.")
      .ScalePrecision(2, 12)
      .WithMessage("O valor financiado deve conter 2 casas decimais");

      RuleFor(Cadastro => Cadastro.Logradouro)
      .NotEmpty()
      .WithMessage("O logradouro não pode ser nulo")
      .Length(0, 60)
      .WithMessage("O logradouro deve ter no máximo 60 caracteres");

      RuleFor(Cadastro => Cadastro.Numero_residencia)
      .NotEmpty()
      .WithMessage("O número não pode ser nulo")
      .Length(0, 10)
      .WithMessage("O número deve ter no máximo 10 caracteres");

      RuleFor(Cadastro => Cadastro.Bairro)
      .NotEmpty()
      .WithMessage("O bairro não pode ser nulo")
      .Length(0, 60)
      .WithMessage("O bairo deve ter no máximo 60 caracteres");

      RuleFor(Cadastro => Cadastro.Cidade)
      .NotEmpty()
      .WithMessage("A cidade não pode ser nulo")
      .Length(0, 60)
      .WithMessage("A cidade deve ter no máximo 60 caracteres");

      RuleFor(Cadastro => Cadastro.Cep)
      .NotEmpty()
      .WithMessage("O cep não pode ser nulo")
      .Length(8)
      .WithMessage("O cep deve ter conter 08 caracteres");

      RuleFor(Cadastro => Cadastro.Proposta)
      .ScalePrecision(0, 10)
      .WithMessage("O valor solicitado não deve conter casas decimais");

      RuleFor(Cadastro => Cadastro.Conveniada)
      .NotNull()
      .WithMessage("A conveniada não pode ser nula")
      .Length(6)
      .WithMessage("A conveniada deve conter 06 caracteres");

      RuleFor(Cadastro => Cadastro.Vlr_solicitado)
      .ScalePrecision(2, 12)
      .WithMessage("O valor solicitado deve conter 02 casas decimais");

      RuleFor(Cadastro => Cadastro.Vlr_financiado)
      .ScalePrecision(2, 12)
      .WithMessage("O valor financiado deve conter 02 casas decimais");

      RuleFor(Cadastro => Cadastro.Situacao)
      .NotEmpty()
      .WithMessage("A situaçao não pode ser nula")
      .Length(2)
      .WithMessage("A situaçao deve ter 02 caracteres");

      RuleFor(Cadastro => Cadastro.Observacao)
      .Length(0, 500)
      .WithMessage("A situacao deve ter no máximo 500 caracteres");

      RuleFor(Cadastro => Cadastro.Dt_situacao)
      .NotNull()
      .WithMessage("A data da situação não pode ser nula.");

      RuleFor(Cadastro => Cadastro.Usuario)
      .NotNull()
      .WithMessage("O username não pode ser nulo")
      .Length(0, 10)
      .WithMessage("O username deve ter no máximo 10 caracteres");
    }
  }
}
