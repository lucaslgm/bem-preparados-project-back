using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
  public class R_Usuario : RepositoryConfiguration, IBaseRepository<E_Usuario>, IR_Usuario
  {
    public R_Usuario(IConfiguration configuration) : base(configuration) { }


    public Task<bool> Delete(int id)
    {
      throw new System.NotImplementedException();
    }
    public Task<int> Update(E_Usuario obj)
    {
      throw new System.NotImplementedException();
    }

    #region WORKING
    public async Task<E_Usuario> Get(int id)
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_USUARIOS] WHERE id_treina_usuario = @Id;";
      string sqlQuery = "SELECT id_treina_usuario, usuario, senha, nome, validade_senha " +
                        "FROM [dbo].[TREINA_USUARIOS] WHERE id_treina_usuario = @Id;";

      E_Usuario ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Usuario>(sqlQuery, new { Id = id });
      }
      return ret;
    }
    public async Task<IEnumerable<E_Usuario>> Get()
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_USUARIOS]";
      string sqlQuery = "SELECT id_treina_usuario, usuario, senha, nome, validade_senha " +
                        "FROM [dbo].[TREINA_USUARIOS];";

      IEnumerable<E_Usuario> ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QueryAsync<E_Usuario>(sqlQuery);
      }
      return ret;
    }
    public virtual async Task<E_Usuario> GetByUsername(string username)
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_USUARIOS] WHERE usuario= @usuario";
      string sqlQuery = "SELECT id_treina_usuario, usuario, senha, nome, validade_senha " +
                        "FROM [dbo].[TREINA_USUARIOS] WHERE usuario = @usuario;";

      E_Usuario ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Usuario>(sqlQuery, new { usuario = username });
      }
      return ret;
    }
    public async Task<E_Usuario> Insert(E_Usuario obj)
    {
      string sqlQuery = "INSERT INTO [dbo].[TREINA_USUARIOS] (usuario,senha,nome,validade_senha,data_atualizacao,usuario_atualizacao) VALUES (@usuario,@senha,@nome,@validade_senha,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT)";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@usuario", obj.Usuario);
      parameter.Add("@senha", obj.Senha);
      parameter.Add("@nome", obj.Nome);
      parameter.Add("@validade_senha", DateTime.UtcNow.AddMonths(3));
      parameter.Add("@data_atualizacao", DateTime.UtcNow);
      parameter.Add("@usuario_atualizacao", "SISTEMA");

      E_Usuario ret = new E_Usuario();

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret.Id_treina_usuario = await connection.QuerySingleAsync<int>(sqlQuery, parameter);
      }
      return ret;
    }

    #endregion
  }
}
