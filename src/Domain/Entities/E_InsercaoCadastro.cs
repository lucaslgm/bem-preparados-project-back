using System;
using System.Collections.Generic;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_InsercaoCadastro : E_Base
  {
    public int Id_treina_cliente { get; set; }
    public int Id_treina_proposta { get; set; }
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime Dt_nascimento { get; set; }
    public char Genero { get; set; }
    public decimal Vlr_salario { get; set; }
    public string Logradouro { get; set; }
    public string Numero_residencia { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Cep { get; set; }
    public decimal Proposta { get; set; }
    public string Conveniada { get; set; }
    public decimal Vlr_solicitado { get; set; }
    public short Prazo { get; set; }
    public decimal Vlr_financiado { get; set; }
    public string Situacao { get; set; }
    public string Observacao { get; set; }
    public DateTime Dt_situacao { get; set; }
    public string Usuario { get; set; }

    public E_InsercaoCadastro(int id_treina_cliente, int id_treina_proposta, string cpf, string nome, DateTime dt_nascimento, char genero, decimal vlr_salario, string logradouro, string numero_residencia, string bairro, string cidade, string cep, decimal proposta, string conveniada, decimal vlr_solicitado, short prazo, decimal vlr_financiado, string situacao, string observacao, DateTime dt_situacao, string usuario)
    {
      Id_treina_cliente = id_treina_cliente;
      Id_treina_proposta = id_treina_proposta;
      Cpf = cpf;
      Nome = nome;
      Dt_nascimento = dt_nascimento;
      Genero = genero;
      Vlr_salario = vlr_salario;
      Logradouro = logradouro;
      Numero_residencia = numero_residencia;
      Bairro = bairro;
      Cidade = cidade;
      Cep = cep;
      Proposta = proposta;
      Conveniada = conveniada;
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

    public E_InsercaoCadastro()
    {

    }

    public override bool Validate()
    {
      var validator = new V_InsercaoCadastro();
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
