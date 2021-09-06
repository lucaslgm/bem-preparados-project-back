using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Dtos
{
  /*
    => Dtos servem pra transitar dados entre camadas
    => Não possuem métodos
    => Utilizado na comunicação entre a camada de Serviços e a de Aplicação
    => Comunicação entre a camada de Serviço com Domínio e Infra pode ser feita através das Entities
  */

  public class D_Usuario
  {
    public int Id_treina_usuario { get; set; }
    public String Usuario { get; set; }
    public String Senha { get; set; }
    public String Nome { get; set; }
    public DateTime Validade_senha { get; set; }
    public DateTime Data_atualizacao { get; set; }
    public String Usuario_atualizacao { get; set; }

    public D_Usuario() { }

    public D_Usuario(int id_treina_usuario, string usuario, string senha, string nome, DateTime validade_senha, DateTime data_atualizacao, string usuario_atualizacao)
    {
      Usuario = usuario;
      Senha = senha;
      Nome = nome;
      Id_treina_usuario = id_treina_usuario;
      Validade_senha = validade_senha;
      Data_atualizacao = data_atualizacao;
      Usuario_atualizacao = usuario_atualizacao;
    }
  }
}
