using System.Threading.Tasks;

namespace Service.Interfaces
{
  public interface IS_Cadastro
  {
    Task<decimal> CalculaValorFinanciado(double vlrSolicitado, int prazo);
  }
}
