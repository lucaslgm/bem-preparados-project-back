using Domain.Entities;
using FluentValidation;

namespace Domain.Validators
{
  public class V_PropostaConveniada : AbstractValidator<E_PropostaCompleta>
  {
    public V_PropostaConveniada()
    {
      RuleFor(Proposta => Proposta.Proposta)
      .NotNull()
      .WithMessage("A proposta não pode ser nula")
      .ScalePrecision(0, 10)
      .WithMessage("O valor solicitado não deve conter casas decimais");

      RuleFor(Proposta => Proposta.Cpf)
      .NotEmpty()
      .WithMessage("O CPF não pode ser nulo")
      .Length(11)
      .WithMessage("O CPF deve conter 11 caracteres");

      RuleFor(Proposta => Proposta.Nome)
      .NotEmpty()
      .WithMessage("O Nome não pode ser nulo")
      .Length(60)
      .WithMessage("O CPF deve conter no máximo 60 caracteres");

      RuleFor(Proposta => Proposta.Conveniada)
      .NotNull()
      .WithMessage("A conveniada não pode ser nula")
      .Length(20)
      .WithMessage("O CPF deve conter no máximo 20 caracteres");

      RuleFor(Proposta => Proposta.Vlr_solicitado)
      .ScalePrecision(2, 12)
      .WithMessage("O valor solicitado deve conter 2 casas decimais");

      RuleFor(Proposta => Proposta.Vlr_financiado)
      .ScalePrecision(2, 12)
      .WithMessage("O valor financiado deve conter 2 casas decimais");

      RuleFor(Proposta => Proposta.Situacao)
      .NotNull()
      .WithMessage("A situaçao não pode ser nula")
      .Length(25)
      .WithMessage("O CPF deve conter no máximo 25 caracteres"); ;

      RuleFor(Proposta => Proposta.Observacao)
      .Length(0, 500)
      .WithMessage("A situacao deve ter no máximo 500 caracteres");

      RuleFor(Proposta => Proposta.Dt_situacao)
      .NotNull()
      .WithMessage("A data da situação não pode ser nula.");

      RuleFor(Proposta => Proposta.Usuario)
      .NotNull()
      .WithMessage("O username não pode ser nulo")
      .Length(0, 10)
      .WithMessage("O username deve ter no máximo 10 caracteres");
    }
  }
}
