using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_AtualizacaoCadastro
  {
    [Required(ErrorMessage = "CPF é um campo obrigatório")]
    [MinLength(11, ErrorMessage = "CPF deve ter no mínimo {1} caracteres.")]
    [MaxLength(11, ErrorMessage = "CPF deve ter no máximo {1} caracteres.")]
    [StringLength(11, ErrorMessage = "CPF deve ter no máximo {1} caracteres.")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatório")]
    [StringLength(60, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Data de nascimento é um campo obrigatório")]
    [Range(typeof(DateTime), "01-01-1900", "06-06-2079")]
    public DateTime Dt_nascimento { get; set; }

    [Required(ErrorMessage = "Gênero é um campo obrigatório")]
    // [MinLength(1, ErrorMessage = "Gênero deve ter {1} caracteres.")]
    // [MaxLength(1, ErrorMessage = "Gênero deve ter {1} caracteres.")]
    public char Genero { get; set; }

    [Required(ErrorMessage = "Salário é um campo obrigatório")]
    [Range(0, 9999999999.99)]
    public decimal Vlr_salario { get; set; }

    [Required(ErrorMessage = "Logradouro é um campo obrigatório")]
    [StringLength(60, ErrorMessage = "Logradouro deve ter no máximo {1} caracteres.")]
    public string Logradouro { get; set; }

    [Required(ErrorMessage = "Número é um campo obrigatório")]
    [StringLength(10, ErrorMessage = "Número deve ter no máximo {1} caracteres.")]
    public string Numero_residencia { get; set; }

    [Required(ErrorMessage = "Bairro é um campo obrigatório")]
    [StringLength(60, ErrorMessage = "Bairro deve ter no máximo {1} caracteres.")]
    public string Bairro { get; set; }

    [Required(ErrorMessage = "Cidade é um campo obrigatório")]
    [StringLength(60, ErrorMessage = "Cidade deve ter no máximo {1} caracteres.")]
    public string Cidade { get; set; }

    [Required(ErrorMessage = "CEP é um campo obrigatório")]
    [MinLength(8, ErrorMessage = "CEP deve ter {1} caracteres.")]
    [MaxLength(8, ErrorMessage = "CEP deve ter {1} caracteres.")]
    public string Cep { get; set; }

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
