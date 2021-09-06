using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
  public class M_AtualizacaoUsuario
  {
    [Required(ErrorMessage = "O Id não pode ser vazio.")]
    [Range(1, int.MaxValue, ErrorMessage = "O id não pode ser menor que 1.")]
    public int Id_treina_usuario { get; set; }
    [Required(ErrorMessage = "Username é um campo obrigatório")]
    [StringLength(10, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public String Usuario { get; set; }

    [Required(ErrorMessage = "Senha é um campo obrigatório")]
    [StringLength(10, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public String Senha { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatório")]
    [StringLength(20, ErrorMessage = "Nome deve ter no máximo {1} caracteres.")]
    public String Nome { get; set; }
  }
}
