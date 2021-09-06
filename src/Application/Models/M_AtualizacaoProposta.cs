using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_AtualizacaoProposta
  {
    [Required(ErrorMessage = "Proposta é um campo obrigatório")]
    public string Proposta { get; set; }

    [Required(ErrorMessage = "Conveniada é um campo obrigatório")]
    public string Conveniada { get; set; }

    [Required(ErrorMessage = "Vlr_solicitado é um campo obrigatório")]
    public decimal Vlr_solicitado { get; set; }

    [Required(ErrorMessage = "Prazo é um campo obrigatório")]
    public short Prazo { get; set; }

    [Required(ErrorMessage = "Vlr_financiado é um campo obrigatório")]
    public decimal Vlr_financiado { get; set; }

    [Required(ErrorMessage = "Usuario é um campo obrigatório")]
    public string Usuario { get; set; }
  }
}
