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
  public class R_Proposta : RepositoryConfiguration, IBaseRepository<E_Proposta>, IR_Proposta
  {
    public R_Proposta(IConfiguration configuration) : base(configuration)
    {
    }
    public async Task<E_Proposta> Insert(E_Proposta obj)
    {

      string sqlQuery = "UPDATE [dbo].[TREINA_PARAMETROS] SET NUMERO_PROPOSTA = NUMERO_PROPOSTA + 1, @proposta = NUMERO_PROPOSTA WHERE NUMERO_PROPOSTA IS NOT NULL;" +
      "INSERT INTO [dbo].[TREINA_PROPOSTAS] (proposta,cpf,conveniada,vlr_solicitado,prazo,vlr_financiado,situacao,observacao,dt_situacao,usuario,data_atualizacao,usuario_atualizacao) VALUES (@proposta,@cpf,@conveniada,@vlr_solicitado,@prazo,@vlr_financiado,@situacao,@observacao,@dt_situacao,@usuario,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT);";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@proposta");
      parameter.Add("@cpf", obj.Cpf);
      parameter.Add("@conveniada", obj.Conveniada);
      parameter.Add("@vlr_solicitado", obj.Vlr_solicitado);
      parameter.Add("@vlr_financiado", obj.Vlr_financiado);
      parameter.Add("@prazo", obj.Prazo);
      parameter.Add("@situacao", obj.Situacao);
      parameter.Add("@dt_situacao", DateTime.Today);
      parameter.Add("@observacao", obj.Observacao);
      parameter.Add("@usuario", obj.Usuario);
      parameter.Add("@data_atualizacao", DateTime.UtcNow);
      parameter.Add("@usuario_atualizacao", "SISTEMA");

      E_Proposta retorno = new E_Proposta();

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno.Id_treina_proposta = await connection.QuerySingleAsync<int>(sqlQuery, parameter);
      }
      return retorno;
    }

    public async Task<IEnumerable<E_Proposta>> Get()
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_PROPOSTAS]";
      string sqlQuery = "SELECT id_treina_proposta, proposta, cpf, conveniada, vlr_solicitado, prazo, vlr_financiado, " +
                        "situacao, observacao, dt_situacao, usuario FROM [dbo].[TREINA_PROPOSTAS]";

      IEnumerable<E_Proposta> retorno;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.QueryAsync<E_Proposta>(sqlQuery);
      }
      return retorno;
    }

    public async Task<E_Proposta> GetByProposta(decimal proposta)
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_PROPOSTAS] WHERE proposta= @proposta";
      string sqlQuery = "SELECT id_treina_proposta, proposta, cpf, conveniada, vlr_solicitado, prazo, vlr_financiado, " +
                        "situacao, observacao, dt_situacao, usuario FROM [dbo].[TREINA_PROPOSTAS] WHERE proposta= @proposta";

      E_Proposta retorno;

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@proposta", proposta);

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.QuerySingleOrDefaultAsync<E_Proposta>(sqlQuery, parameter);
      }
      return retorno;
    }

    public async Task<E_Proposta> GetByCliente(string cpf)
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_PROPOSTAS] WHERE cpf= @cpf";
      string sqlQuery = "SELECT id_treina_proposta, proposta, cpf, conveniada, vlr_solicitado, prazo, vlr_financiado, " +
                        "situacao, observacao, dt_situacao, usuario FROM [dbo].[TREINA_PROPOSTAS] WHERE cpf= @cpf";

      E_Proposta ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Proposta>(sqlQuery, new { cpf = cpf });
      }
      return ret;
    }

    public async Task<IEnumerable<E_Proposta>> GetByUsuario(string usuario)
    {
      // string sqlQuery = "SELECT * FROM [dbo].[TREINA_PROPOSTAS] WHERE usuario = @usuario;";
      string sqlQuery = "SELECT id_treina_proposta, proposta, cpf, conveniada, vlr_solicitado, prazo, vlr_financiado, " +
                        "situacao, observacao, dt_situacao, usuario FROM [dbo].[TREINA_PROPOSTAS] WHERE usuario= @usuario";

      IEnumerable<E_Proposta> retorno;

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@usuario", usuario);


      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.QueryAsync<E_Proposta>(sqlQuery, parameter);
      }
      return retorno;
    }

    public async Task<int> Update(E_Proposta obj)
    {
      string sqlQuery = "UPDATE [dbo].[TREINA_PROPOSTAS] SET CONVENIADA = @conveniada, " +
      "VLR_SOLICITADO = @vlr_solicitado, PRAZO = @prazo, VLR_FINANCIADO = @vlr_financiado, " +
      "USUARIO = @usuario, SITUACAO = @situacao, DT_SITUACAO = @dt_situacao, " +
      "DATA_ATUALIZACAO = @data_atualizacao WHERE PROPOSTA = @proposta;";

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@proposta", obj.Proposta);
      parameter.Add("@conveniada", obj.Conveniada);
      parameter.Add("@vlr_solicitado", obj.Vlr_solicitado);
      parameter.Add("@vlr_financiado", obj.Vlr_financiado);
      parameter.Add("@prazo", obj.Prazo);
      parameter.Add("@situacao", "AG");
      parameter.Add("@dt_situacao", DateTime.Today);
      parameter.Add("@usuario", obj.Usuario);
      parameter.Add("@data_atualizacao", DateTime.UtcNow);

      int retorno;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.ExecuteAsync(sqlQuery, parameter);
      }
      return retorno;
    }

    public Task<bool> Delete(int id)
    {
      throw new System.NotImplementedException();
    }

    public async Task<decimal> GetNumeroProposta(int id)
    {
      string sqlQuery = "SELECT PROPOSTA FROM [dbo].[TREINA_PROPOSTAS] WHERE id_treina_proposta = @Id;";

      E_Proposta ret;

      using (var connection = new SqlConnection(base.GetConnection()))
      {
        ret = await connection.QuerySingleOrDefaultAsync<E_Proposta>(sqlQuery, new { Id = id });
      }

      return ret.Proposta;
    }
  }
}
