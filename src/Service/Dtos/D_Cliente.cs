using System;

namespace Service.Dtos
{
  public class D_Cliente
  {
    public int Id_treina_cliente { get; set; }
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime Dt_nascimento { get; set; }
    public char Genero { get; set; }
    public decimal Vlr_salario { get; set; }
    public string Logradouro { get; set; }
    public string Numero_residencia { get; set; }
    public string Bairro { get; set; }
    public string Cidade { get; set; }
    public string Cep { get; set; }
    public DateTime Data_atualizacao { get; set; }
    public string Usuario_atualizacao { get; set; }
  }
}
