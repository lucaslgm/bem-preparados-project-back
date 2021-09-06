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

    [Required(ErrorMessage = "CPF é um campo obrigatório")]
    [MinLength(11, ErrorMessage = "CPF deve ter no mínimo {1} caracteres.")]
    [MaxLength(11, ErrorMessage = "CPF deve ter no máximo {1} caracteres.")]
    [StringLength(11, ErrorMessage = "CPF deve ter no máximo {1} caracteres.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "Situacao é um campo obrigatório")]
    public string Situacao { get; set; }
  }
}
