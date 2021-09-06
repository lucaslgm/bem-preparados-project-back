using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_Login
  {
    Task<string> ValidateLogin(E_Login usuario);
  }
}
