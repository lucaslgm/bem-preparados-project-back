using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_Conveniada
  {
    [Required(ErrorMessage = "Conveniada é um campo obrigatório")]
    [StringLength(6, ErrorMessage = "Conveniada deve ter no máximo {1} caracteres.")]
    public string Conveniada { get; set; }
  }
}
