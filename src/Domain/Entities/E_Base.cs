using System;
using System.Collections.Generic;

namespace Domain.Entities
{
  public abstract class E_Base
  {
    private string? _usuario_atualizacao;
    public string? Usuario_atualizacao
    {
      get { return _usuario_atualizacao; }
      set { _usuario_atualizacao = (value == null ? "SISTEMA" : value); }
    }

    private DateTime? _data_atualizacao;
    public DateTime? Data_atualizacao
    {
      get { return _data_atualizacao; }
      set { _data_atualizacao = value == null ? DateTime.UtcNow : value; }
    }

    internal List<string> _erros;
    public IReadOnlyCollection<string> Erros => _erros;

    public abstract bool Validate();
  }
}
