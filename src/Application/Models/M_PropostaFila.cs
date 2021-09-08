using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_PropostaFila
  {
    [Required(ErrorMessage = "Data de nascimento é um campo obrigatório")]
    [Range(typeof(DateTime), "01-01-1900", "06-06-2079")]
    public DateTime Dt_nascimento { get; set; }

    [Required(ErrorMessage = "Proposta é um campo obrigatório")]
    public string Proposta { get; set; }

    [Required(ErrorMessage = "Conveniada é um campo obrigatório")]
    public string Conveniada { get; set; }

    [Required(ErrorMessage = "Vlr_solicitado é um campo obrigatório")]
    public decimal Vlr_solicitado { get; set; }

    [Required(ErrorMessage = "Prazo é um campo obrigatório")]
    public short Prazo { get; set; }
  }
}
