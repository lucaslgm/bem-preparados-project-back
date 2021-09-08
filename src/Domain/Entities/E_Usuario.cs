using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_Usuario : E_Base
  {
    public int Id_treina_usuario { get; set; }

    public String Usuario { get; set; }

    public String Senha { get; set; }

    public String Nome { get; set; }

    private DateTime? _validade_senha;
    public DateTime? Validade_senha
    {
      get { return _validade_senha; }
      set { _validade_senha = value == null ? (DateTime.UtcNow.AddMonths(3)) : value; }
    }
    public E_Usuario(String username, String password, String name)
    {
      this.Usuario = username;
      this.Senha = password;
      this.Nome = name;
      this._erros = new List<string>();
      Validate();
    }

    public E_Usuario() { }
    public override bool Validate()
    {
      var validator = new V_Usuario();
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
