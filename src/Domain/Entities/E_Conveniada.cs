using System.Collections.Generic;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_Conveniada : E_Base
  {
    public int Id_treina_conveniada { get; set; }
    public string conveniada { get; set; }
    public string descricao { get; set; }

    public E_Conveniada(int id_treina_conveniada, string conveniada, string descricao)
    {
      Id_treina_conveniada = id_treina_conveniada;
      this.conveniada = conveniada;
      this.descricao = descricao;
      this._erros = new List<string>();

      Validate();
    }

    public E_Conveniada()
    {
    }

    public override bool Validate()
    {
      var validator = new V_Conveniada();
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
