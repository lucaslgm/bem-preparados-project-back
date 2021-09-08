using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Cadastro
  {
    Task<decimal> CalculaValorFinanciado(double vlrSolicitado, int prazo);

    void SendtoQueueAsync(D_PropostaFila dto);

    Task<D_InsercaoCadastro> Insert(D_InsercaoCadastro dto);
    Task<int> Update(D_AtualizacaoCadastro dto);
  }
}
