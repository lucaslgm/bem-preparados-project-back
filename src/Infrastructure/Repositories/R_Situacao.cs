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
  public class R_Situacao : RepositoryConfiguration, IBaseRepository<E_Situacao>, IR_Situacao
  {
    public R_Situacao(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<E_Situacao> Insert(E_Situacao obj)
    {
      string sqlQuery = "INSERT INTO [dbo].[TREINA_SITUACAO] (situacao, descricao,data_atualizacao,usuario_atualizacao) VALUES (@situacao,@descricao,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT)";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@situacao", obj.situacao);
      parameter.Add("@descricao", obj.descricao);
      parameter.Add("@data_atualizacao", DateTime.UtcNow);
      parameter.Add("@usuario_atualizacao", "SISTEMA");

      E_Situacao ret = new E_Situacao();

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret.Id_treina_situacao = await connection.QuerySingleAsync<int>(sqlQuery, parameter);
      }
      return ret;
    }
    public async Task<E_Situacao> Get(string situacao)
    {
      string sqlQuery = "SELECT * FROM [dbo].[TREINA_SITUACAO] WHERE SITUACAO = @situacao;";

      E_Situacao ret;

      DynamicParameters parameters = new DynamicParameters();
      parameters.Add("@situacao", situacao);

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Situacao>(sqlQuery, parameters);
      }
      return ret;
    }
    public async Task<IEnumerable<E_Situacao>> Get()
    {
      string sqlQuery = "SELECT * FROM [dbo].[TREINA_SITUACAO]";

      IEnumerable<E_Situacao> ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryAsync<E_Situacao>(sqlQuery);
      }
      return ret;
    }
    public async Task<E_Situacao> GetByState(string situacao)
    {
      string sqlQuery = "SELECT * FROM [dbo].[TREINA_SITUACAO] WHERE situacao= @situacao";

      E_Situacao ret;

      DynamicParameters parameters = new DynamicParameters();
      parameters.Add("@situacao", situacao);

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Situacao>(sqlQuery, parameters);
      }
      return ret;
    }

    public Task<int> Update(E_Situacao obj)
    {
      throw new System.NotImplementedException();
    }
    public Task<bool> Delete(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}
