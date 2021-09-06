using System.Collections.Generic;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_Situacao : E_Base
  {

    public int Id_treina_situacao { get; set; }
    public string situacao { get; set; }
    public string descricao { get; set; }
    public E_Situacao(int id_treina_situacao, string situacao, string descricao)
    {
      Id_treina_situacao = id_treina_situacao;
      this.situacao = situacao;
      this.descricao = descricao;
      this._erros = new List<string>();

      Validate();
    }
    public E_Situacao()
    {
    }
    public override bool Validate()
    {
      var validator = new V_Situacao();
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
