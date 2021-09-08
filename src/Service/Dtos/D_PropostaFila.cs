using System;
using System.ComponentModel.DataAnnotations;

namespace Service.Dtos
{
  public class D_PropostaFila
  {
    public DateTime Dt_nascimento { get; set; }

    public string Proposta { get; set; }

    public string Conveniada { get; set; }

    public decimal Vlr_solicitado { get; set; }

    public short Prazo { get; set; }
  }
}
