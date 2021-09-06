using System;

namespace Service.Dtos
{
  public class D_Proposta
  {
    public decimal Proposta { get; set; }
    public string Conveniada { get; set; }
    public string Cpf { get; set; }
    public decimal Vlr_solicitado { get; set; }
    public short Prazo { get; set; }
    public decimal Vlr_financiado { get; set; }
    public string Situacao { get; set; }
    public string Observacao { get; set; }
    public DateTime Dt_situacao { get; set; }
    public string Usuario { get; set; }
  }
}
