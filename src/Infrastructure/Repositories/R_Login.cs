using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
  public class R_Login : RepositoryConfiguration, IR_Login
  {

    public R_Login(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<string> ValidateLogin(E_Login obj)
    {
      string sqlQuery = "SELECT [dbo].[F_validar_usuario] (@usuario, @senha) FROM TREINA_USUARIOS WHERE USUARIO = @usuario;";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@usuario", obj.Usuario);
      parameter.Add("@senha", obj.Senha);

      var retorno = "";

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.QuerySingleOrDefaultAsync<string>(sqlQuery, parameter);
      }
      return retorno;
    }
  }
}
