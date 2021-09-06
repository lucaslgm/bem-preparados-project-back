using System.Threading.Tasks;

namespace Service.Interfaces
{
  public interface IS_ViaCep
  {
    Task<object> GetEndereco(string cep);
  }
}
