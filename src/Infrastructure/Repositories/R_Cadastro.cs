using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories
{
  public class R_Cadastro : RepositoryConfiguration, IR_Cadastro
  {
    public R_Cadastro(IConfiguration configuration) : base(configuration)
    {
    }

    public Task<bool> Delete(int id)
    {
      throw new System.NotImplementedException();
    }

    public Task<IEnumerable<E_ClienteProposta>> Get()
    {
      throw new System.NotImplementedException();
    }

    public async Task<E_ClienteProposta> Insert(E_ClienteProposta obj)
    {
      #region sqlQueries
      string sqlQueryCli = "INSERT INTO [dbo].[TREINA_CLIENTES] (cpf,nome,dt_nascimento,genero,vlr_salario,logradouro,numero_residencia,bairro,cidade,cep,data_atualizacao,usuario_atualizacao) VALUES (@cpf,@nome,@dt_nascimento,@genero,@vlr_salario,@logradouro,@numero_residencia,@bairro,@cidade,@cep,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT)";

      string sqlQueryProp = "UPDATE [dbo].[TREINA_PARAMETROS] SET NUMERO_PROPOSTA = NUMERO_PROPOSTA + 1, @proposta = NUMERO_PROPOSTA WHERE NUMERO_PROPOSTA IS NOT NULL;" +
      "INSERT INTO [dbo].[TREINA_PROPOSTAS] (proposta,cpf,conveniada,vlr_solicitado,prazo,vlr_financiado,situacao,observacao,dt_situacao,usuario,data_atualizacao,usuario_atualizacao) VALUES (@proposta,@cpf,@conveniada,@vlr_solicitado,@prazo,@vlr_financiado,@situacao,@observacao,@dt_situacao,@usuario,@data_atualizacao,@usuario_atualizacao); SELECT CAST(SCOPE_IDENTITY() AS INT);";
      #endregion

      #region Parameters
      DynamicParameters cliParams = new DynamicParameters();
      cliParams.Add("@cpf", obj.Cpf);
      cliParams.Add("@nome", obj.Nome);
      cliParams.Add("@dt_nascimento", obj.Dt_nascimento);
      cliParams.Add("@genero", obj.Genero);
      cliParams.Add("@vlr_salario", obj.Vlr_salario);
      cliParams.Add("@logradouro", obj.Logradouro);
      cliParams.Add("@numero_residencia", obj.Numero_residencia);
      cliParams.Add("@bairro", obj.Bairro);
      cliParams.Add("@cidade", obj.Cidade);
      cliParams.Add("@cep", obj.Cep);
      cliParams.Add("@data_atualizacao", DateTime.UtcNow);
      cliParams.Add("@usuario_atualizacao", "SISTEMA");

      DynamicParameters propParams = new DynamicParameters();
      propParams.Add("@proposta");
      propParams.Add("@cpf", obj.Cpf);
      propParams.Add("@conveniada", obj.Conveniada);
      propParams.Add("@vlr_solicitado", obj.Vlr_solicitado);
      propParams.Add("@vlr_financiado", obj.Vlr_financiado);
      propParams.Add("@prazo", obj.Prazo);
      propParams.Add("@situacao", obj.Situacao);
      propParams.Add("@dt_situacao", DateTime.Today);
      propParams.Add("@observacao", obj.Observacao);
      propParams.Add("@usuario", obj.Usuario);
      propParams.Add("@data_atualizacao", DateTime.UtcNow);
      propParams.Add("@usuario_atualizacao", "SISTEMA");
      #endregion

      E_ClienteProposta ret = new E_ClienteProposta();

      using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
      {
        using (var connection = new SqlConnection(base.GetConnection()))
        {
          ret.Id_treina_cliente = await connection.QuerySingleAsync<int>(sqlQueryCli, cliParams);
          ret.Id_treina_proposta = await connection.QuerySingleAsync<int>(sqlQueryProp, propParams);
        }
        transaction.Complete();
      }
      return ret;
    }

    public async Task<int> Update(E_ClienteProposta obj)
    {
      #region sqlQueries
      string sqlQueryCli = "UPDATE [dbo].[TREINA_CLIENTES] SET NOME = @nome, DT_NASCIMENTO = @dt_nascimento, " +
      "GENERO = @genero, VLR_SALARIO = @vlr_salario, LOGRADOURO = @logradouro, NUMERO_RESIDENCIA = @numero_residencia, " +
      "BAIRRO = @bairro, CIDADE = @cidade, CEP = @cep, DATA_ATUALIZACAO = @data_atualizacao WHERE cpf = @cpf;";

      string sqlQueryProp = "UPDATE [dbo].[TREINA_PROPOSTAS] SET CONVENIADA = @conveniada, " +
      "VLR_SOLICITADO = @vlr_solicitado, PRAZO = @prazo, VLR_FINANCIADO = @vlr_financiado, " +
      "USUARIO = @usuario, SITUACAO = @situacao, DT_SITUACAO = @dt_situacao, " +
      "DATA_ATUALIZACAO = @data_atualizacao WHERE PROPOSTA = @proposta;";

      #endregion

      #region Parameters
      DynamicParameters cliParams = new DynamicParameters();
      cliParams.Add("@cpf", obj.Cpf);
      cliParams.Add("@nome", obj.Nome);
      cliParams.Add("@dt_nascimento", obj.Dt_nascimento);
      cliParams.Add("@genero", obj.Genero);
      cliParams.Add("@vlr_salario", obj.Vlr_salario);
      cliParams.Add("@logradouro", obj.Logradouro);
      cliParams.Add("@numero_residencia", obj.Numero_residencia);
      cliParams.Add("@bairro", obj.Bairro);
      cliParams.Add("@cidade", obj.Cidade);
      cliParams.Add("@cep", obj.Cep);
      cliParams.Add("@data_atualizacao", DateTime.UtcNow);

      DynamicParameters propParams = new DynamicParameters();
      propParams.Add("@proposta", obj.Proposta);
      propParams.Add("@conveniada", obj.Conveniada);
      propParams.Add("@vlr_solicitado", obj.Vlr_solicitado);
      propParams.Add("@vlr_financiado", obj.Vlr_financiado);
      propParams.Add("@prazo", obj.Prazo);
      propParams.Add("@situacao", obj.Situacao);
      propParams.Add("@dt_situacao", DateTime.Today);
      propParams.Add("@usuario", obj.Usuario);
      propParams.Add("@data_atualizacao", DateTime.UtcNow);
      #endregion

      int ret;

      using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
      {
        using (var connection = new SqlConnection(base.GetConnection()))
        {
          ret = await connection.ExecuteAsync(sqlQueryCli, cliParams);
          ret = ret + await connection.ExecuteAsync(sqlQueryProp, propParams);
        }
        transaction.Complete();
      }
      return ret;
    }
  }
}
