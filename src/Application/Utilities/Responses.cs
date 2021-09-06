using System.Collections.Generic;
using Application.Models;

namespace Application.Utilities
{
  public class Responses
  {
    public static M_Resultado ApplicationErrorMessage()
    {
      return new M_Resultado
      {
        Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente.",
        Success = false,
        Data = null
      };
    }

    public static M_Resultado DomainErrorMessage(string message)
    {
      return new M_Resultado
      {
        Message = message,
        Success = false,
        Data = null
      };
    }

    public static M_Resultado DomainErrorMessage(string message, IReadOnlyCollection<string> errors)
    {
      return new M_Resultado
      {
        Message = message,
        Success = false,
        Data = errors
      };
    }

    public static M_Resultado UnauthorizedErrorMessage()
    {
      return new M_Resultado
      {
        Message = "A combinação de login e senha está incorreta!",
        Success = false,
        Data = null
      };
    }
  }
}
