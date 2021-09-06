using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Login
  {
    Task<object> ValidateLogin(D_Login usuario);
  }
}
