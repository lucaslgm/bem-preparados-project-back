using System;
using System.Collections.Generic;
using CrossCutting.Exceptions;
using Domain.Validators;

namespace Domain.Entities
{
  public class E_Login : E_Base
  {
    public String Usuario { get; set; }

    public String Senha { get; set; }
    public E_Login(String usuario, String senha)
    {
      this.Usuario = usuario;
      this.Senha = senha;

      Validate();

    }
    public override bool Validate()
    {
      var validator = new V_Login();
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
