using System;
using System.Collections.Generic;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_Proposta : E_Base
  {
    public int Id_treina_proposta { get; set; }
    public decimal Proposta { get; set; }
    public string Conveniada { get; set; }
    public string Cpf { get; set; }
    public decimal Vlr_solicitado { get; set; }
    public short Prazo { get; set; }
    public decimal Vlr_financiado { get; set; }
    public string Situacao { get; set; }
    public string Observacao { get; set; }
    public DateTime Dt_situacao { get; set; }
    public string Usuario { get; set; }

    public E_Proposta(decimal proposta, string conveniada, string cpf, decimal vlr_solicitado, short prazo, decimal vlr_financiado, string situacao, string observacao, DateTime dt_situacao, string usuario)
    {
      Proposta = proposta;
      Conveniada = conveniada;
      Cpf = cpf;
      Vlr_solicitado = vlr_solicitado;
      Prazo = prazo;
      Vlr_financiado = vlr_financiado;
      Situacao = situacao;
      Observacao = observacao;
      Dt_situacao = dt_situacao;
      Usuario = usuario;
      this._erros = new List<string>();

      Validate();
    }

    public E_Proposta()
    {
    }

    public override bool Validate()
    {
      var validator = new V_Proposta();
      var validation = validator.Validate(this);

      if (!validation.IsValid)
      {
        foreach (var error in validation.Errors)
        {
          _erros.Add(error.ErrorMessage);
        }

        throw new DomainException("Erro:", _erros);

      }

      return true;
    }
  }
}
