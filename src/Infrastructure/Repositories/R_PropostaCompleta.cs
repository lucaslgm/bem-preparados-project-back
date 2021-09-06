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
  public class R_PropostaCompleta : RepositoryConfiguration, IR_PropostaCompleta
  {
    public R_PropostaCompleta(IConfiguration configuration) : base(configuration)
    {
    }

    public async Task<IEnumerable<E_PropostaCompleta>> GetPropostasCompletas(string usuario)
    {
      string sqlQuery = "SELECT cl.cpf, cl.nome, pr.proposta, co.descricao AS conveniada, pr.vlr_solicitado, pr.prazo, " +
      "pr.vlr_financiado, si.descricao AS situacao, pr.observacao, pr.dt_situacao, pr.usuario FROM TREINA_CLIENTES cl " +
      "INNER JOIN TREINA_PROPOSTAS pr ON cl.cpf = pr.cpf " +
      "INNER JOIN TREINA_CONVENIADAS co ON pr.conveniada = co.conveniada " +
      "INNER JOIN TREINA_SITUACAO si on pr.situacao = si.situacao " +
      "WHERE pr.usuario = @usuario; ";

      IEnumerable<E_PropostaCompleta> retorno;

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@usuario", usuario);


      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.QueryAsync<E_PropostaCompleta>(sqlQuery, parameter);
      }
      return retorno;
    }
    public async Task<E_PropostaCompleta> GetPropostaCompleta(string usuario, string cpf)
    {
      string sqlQuery = "SELECT cl.cpf, cl.nome, pr.proposta, co.descricao AS conveniada, pr.vlr_solicitado, pr.prazo, " +
      "pr.vlr_financiado, si.descricao AS situacao, pr.observacao, pr.dt_situacao, pr.usuario FROM TREINA_CLIENTES cl " +
      "INNER JOIN TREINA_PROPOSTAS pr ON cl.cpf = pr.cpf " +
      "INNER JOIN TREINA_CONVENIADAS co ON pr.conveniada = co.conveniada " +
      "INNER JOIN TREINA_SITUACAO si on pr.situacao = si.situacao " +
      "WHERE pr.usuario = @usuario AND pr.cpf = @cpf";

      E_PropostaCompleta retorno;

      DynamicParameters parameter = new DynamicParameters();
      parameter.Add("@usuario", usuario);
      parameter.Add("@cpf", cpf);


      using (var connection = new SqlConnection(base.GetConnection()))
      {
        retorno = await connection.QuerySingleOrDefaultAsync<E_PropostaCompleta>(sqlQuery, parameter);
      }
      return retorno;
    }
  }
}
