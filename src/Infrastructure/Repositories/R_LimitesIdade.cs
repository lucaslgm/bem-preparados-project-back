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
  public class R_LimitesIdade : RepositoryConfiguration, IR_LimitesIdade
  {
    public R_LimitesIdade(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<E_LimitesIdade>> GetByAffiliated(string conveniada)
    {
      string sqlQuery = "SELECT IDADE_INICIAL, IDADE_FINAL, VALOR_LIMITE, PERCENTUAL_MAXIMO_ANALISE FROM [dbo].[TREINA_LIMITES_IDADE_CONVENIADA] WHERE conveniada = @conveniada";

      DynamicParameters parameters = new DynamicParameters();
      parameters.Add("@conveniada", conveniada);

      IEnumerable<E_LimitesIdade> ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryAsync<E_LimitesIdade>(sqlQuery, parameters);
      }
      return ret;
    }

    public async Task<E_LimitesIdade> GetByAge(string conveniada, short idade)
    {
      string sqlQuery = "SELECT VALOR_LIMITE, PERCENTUAL_MAXIMO_ANALISE FROM [dbo].[TREINA_LIMITES_IDADE_CONVENIADA] WHERE conveniada = @conveniada AND IDADE_INICIAL <= @idade AND IDADE_FINAL >= @idade";

      DynamicParameters parameters = new DynamicParameters();
      parameters.Add("@conveniada", conveniada);
      parameters.Add("@idade", idade);

      E_LimitesIdade ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryFirstOrDefaultAsync<E_LimitesIdade>(sqlQuery, parameters);
      }
      return ret;
    }

    public async Task<E_LimitesIdade> GetMinMaxAgeByAffiliated(string conveniada)
    {
      string sqlQuery = "SELECT MIN(IDADE_INICIAL) AS 'IDADE_INICIAL', MAX(IDADE_FINAL) AS 'IDADE_FINAL' from TREINA_LIMITES_IDADE_CONVENIADA where CONVENIADA = @conveniada";

      DynamicParameters parameters = new DynamicParameters();
      parameters.Add("@conveniada", conveniada);

      E_LimitesIdade ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryFirstOrDefaultAsync<E_LimitesIdade>(sqlQuery, parameters);
      }
      return ret;

    }
  }
}
