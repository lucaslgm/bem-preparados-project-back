using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
  public class R_Cliente : RepositoryConfiguration, IBaseRepository<E_Cliente>, IR_Cliente
  {
    public R_Cliente(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<E_Cliente> Insert(E_Cliente obj)
    {
      string sqlQuery = "INSERT INTO [dbo].[TREINA_CLIENTES] (cpf,nome,dt_nascimento,genero,vlr_salario,logradouro,numero_residencia,bairro,cidade,cep,data_atualizacao,usuario_atualizacao) VALUES (@cpf,@nome,@dt_nascimento,@genero,@vlr_salario,@logradouro,@numero_residencia,@bairro,@cidade,@cep,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT)";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@cpf", obj.Cpf);
      parameter.Add("@nome", obj.Nome);
      parameter.Add("@dt_nascimento", obj.Dt_nascimento);
      parameter.Add("@genero", obj.Genero);
      parameter.Add("@vlr_salario", obj.Vlr_salario);
      parameter.Add("@logradouro", obj.Logradouro);
      parameter.Add("@numero_residencia", obj.Numero_residencia);
      parameter.Add("@bairro", obj.Bairro);
      parameter.Add("@cidade", obj.Cidade);
      parameter.Add("@cep", obj.Cep);
      parameter.Add("@data_atualizacao", DateTime.UtcNow);
      parameter.Add("@usuario_atualizacao", "SISTEMA");

      E_Cliente ret = new E_Cliente();

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret.Id_treina_cliente = await connection.QuerySingleAsync<int>(sqlQuery, parameter);
      }
      return ret;
    }

    public async Task<System.Collections.Generic.IEnumerable<E_Cliente>> Get()
    {
      string sqlQuery = "SELECT id_treina_cliente, cpf, nome, dt_nascimento, genero, vlr_salario, bairro, " +
                        "cep, cidade, logradouro, numero_residencia " +
                        "SELECT * FROM [dbo].[TREINA_CLIENTES]";

      IEnumerable<E_Cliente> ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryAsync<E_Cliente>(sqlQuery);
      }
      return ret;
    }

    async Task<E_Cliente> IR_Cliente.GetByCpf(string cpf)
    {
      string sqlQuery = "SELECT id_treina_cliente, cpf, nome, dt_nascimento, genero, vlr_salario, bairro, " +
                        "cep, cidade, logradouro, numero_residencia " +
                        "FROM [dbo].[TREINA_CLIENTES] WHERE cpf= @cpf";
      E_Cliente ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Cliente>(sqlQuery, new { cpf = cpf });
      }
      return ret;
    }

    public async Task<int> Update(E_Cliente obj)
    {
      string sqlQuery = "UPDATE [dbo].[TREINA_CLIENTES] SET NOME = @nome, DT_NASCIMENTO = @dt_nascimento, GENERO = @genero, " +
      "VLR_SALARIO = @vlr_salario, LOGRADOURO = @logradouro, NUMERO_RESIDENCIA = @numero_residencia, BAIRRO = @bairro, " +
      "CIDADE = @cidade, CEP = @cep, DATA_ATUALIZACAO = @data_atualizacao WHERE cpf = @cpf;";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@cpf", obj.Cpf);
      parameter.Add("@nome", obj.Nome);
      parameter.Add("@dt_nascimento", obj.Dt_nascimento);
      parameter.Add("@genero", obj.Genero);
      parameter.Add("@vlr_salario", obj.Vlr_salario);
      parameter.Add("@logradouro", obj.Logradouro);
      parameter.Add("@numero_residencia", obj.Numero_residencia);
      parameter.Add("@bairro", obj.Bairro);
      parameter.Add("@cidade", obj.Cidade);
      parameter.Add("@cep", obj.Cep);
      parameter.Add("@data_atualizacao", DateTime.UtcNow);

      int ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.ExecuteAsync(sqlQuery, parameter);
      }
      return ret;
    }

    public Task<bool> Delete(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}
