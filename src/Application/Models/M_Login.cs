using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_Login
  {
    [Required(ErrorMessage = "Username é um campo obrigatório")]
    [StringLength(10, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public String Usuario { get; set; }

    [Required(ErrorMessage = "Senha é um campo obrigatório")]
    [StringLength(10, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public String Senha { get; set; }
  }
}
