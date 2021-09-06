using System.Collections.Generic;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_LimitesIdade : E_Base
  {

    public int Id_treina_lim_idade_conveniada { get; set; }
    public string Conveniada { get; set; }
    public short Idade_inicial { get; set; }
    public short Idade_final { get; set; }
    public short Percentual_maximo_analise { get; set; }
    public decimal Valor_limite { get; set; }
    public E_LimitesIdade(int id_treina_lim_idade_conveniada, string conveniada, short idade_inicial, short idade_final, short percentual_maximo_analise, decimal valor_limite)
    {
      Id_treina_lim_idade_conveniada = id_treina_lim_idade_conveniada;
      Conveniada = conveniada;
      Idade_inicial = idade_inicial;
      Idade_final = idade_final;
      Percentual_maximo_analise = percentual_maximo_analise;
      Valor_limite = valor_limite;
      this._erros = new List<string>();

      Validate();
    }

    public E_LimitesIdade()
    {
    }

    public override bool Validate()
    {
      var validator = new V_LimitesIdade();
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
