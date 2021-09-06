using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_Situacao : IBaseRepository<E_Situacao>
  {
    Task<E_Situacao> GetByState(string state);
    Task<E_Situacao> Get(string situacao);
  }
}
