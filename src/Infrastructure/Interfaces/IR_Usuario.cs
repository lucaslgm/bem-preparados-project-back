using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  /*
      => para métodos específicos da entidade
      => princípio da segregação de interface
      => melhor ter várias interfaces com métodos especializados do que
      => uma única com todos os métodos
    */
  public interface IR_Usuario : IBaseRepository<E_Usuario>
  {
    Task<E_Usuario> GetByUsername(string username);
    Task<E_Usuario> Get(int id);
  }
}
