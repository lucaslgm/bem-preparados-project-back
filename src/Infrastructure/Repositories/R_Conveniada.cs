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
  public class R_Conveniada : RepositoryConfiguration, IBaseRepository<E_Conveniada>, IR_Conveniada
  {
    public R_Conveniada(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<E_Conveniada> Insert(E_Conveniada obj)
    {
      string sqlQuery = "INSERT INTO [dbo].[TREINA_CONVENIADAS] (conveniada, descricao,data_atualizacao,usuario_atualizacao) VALUES (@conveniada,@descricao,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT)";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@conveniada", obj.conveniada);
      parameter.Add("@descricao", obj.descricao);
      parameter.Add("@data_atualizacao", DateTime.UtcNow);
      parameter.Add("@usuario_atualizacao", "SISTEMA");

      E_Conveniada ret = new E_Conveniada();

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret.Id_treina_conveniada = await connection.QuerySingleAsync<int>(sqlQuery, parameter);
      }
      return ret;
    }
    public async Task<IEnumerable<E_Conveniada>> Get()
    {
      string sqlQuery = "SELECT CONVENIADA, DESCRICAO FROM [dbo].[TREINA_CONVENIADAS]";

      IEnumerable<E_Conveniada> ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryAsync<E_Conveniada>(sqlQuery);
      }
      return ret;
    }
    public async Task<E_Conveniada> GetByAffiliated(string conveniada)
    {
      string sqlQuery = "SELECT CONVENIADA, DESCRICAO FROM [dbo].[TREINA_CONVENIADAS] WHERE conveniada = @conveniada";

      E_Conveniada ret;

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@conveniada", conveniada);

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Conveniada>(sqlQuery, parameter);
      }
      return ret;
    }
    public Task<int> Update(E_Conveniada obj)
    {
      throw new System.NotImplementedException();
    }
    public Task<bool> Delete(int id)
    {
      throw new System.NotImplementedException();
    }
  }
}
