using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_Situacao
  {
    [Required(ErrorMessage = "Situacao é um campo obrigatório")]
    [StringLength(2, ErrorMessage = "Situacao deve ter no máximo {1} caracteres.")]
    public String Situacao { get; set; }
  }
}
