using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
  public class R_Parametros : RepositoryConfiguration, IR_Parametros
  {
    public R_Parametros(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<double> GetTaxaJuros()
    {
      string sqlQuery = "SELECT TAXA_JUROS FROM [dbo].[TREINA_PARAMETROS] WHERE TAXA_JUROS IS NOT NULL";

      double txJuros;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        txJuros = await connection.QuerySingleAsync<int>(sqlQuery);
      }
      return txJuros;
    }
  }
}
