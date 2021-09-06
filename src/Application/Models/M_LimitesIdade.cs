using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_LimitesIdade
  {
    [Range(18, 100, ErrorMessage = "O Cliente deve ser maior de idade.")]
    public short idade_cliente { get; set; }

    [StringLength(6, ErrorMessage = "Conveniada deve ter no máximo {1} caracteres.")]
    [Required]
    public string Conveniada { get; set; }

  }
}
