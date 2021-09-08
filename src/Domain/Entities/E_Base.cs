using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public abstract class E_Base
  {
    public DateTime Data_atualizacao { get; set; }
    public string Usuario_atualizacao { get; set; }

    internal List<string> _erros;
    public IReadOnlyCollection<string> Erros => _erros;

    public abstract bool Validate();
  }
}
