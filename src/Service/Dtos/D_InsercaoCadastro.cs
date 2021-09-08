using System;

namespace Service.Dtos
{
  public class D_InsercaoCadastro
  {
    public int Id_treina_cliente { get; set; }
    public int Id_treina_proposta { get; set; }
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
    public decimal Proposta { get; set; }
    public string Conveniada { get; set; }
    public decimal Vlr_solicitado { get; set; }
    public short Prazo { get; set; }
    public decimal Vlr_financiado { get; set; }
    public string Situacao { get; set; }
    public string Observacao { get; set; }
    public DateTime Dt_situacao { get; set; }
    public string Usuario { get; set; }
    public DateTime Data_atualizacao { get; set; }
    public string Usuario_atualizacao { get; set; }
  }
}
