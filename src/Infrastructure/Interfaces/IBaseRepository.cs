using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{

  /* Task
        => métodos assincronos
        => maior performance
        => threads individuais
        => auto-gerenciáveis
        => facilita o garbage collector
  */
  public interface IBaseRepository<T> where T : E_Base
  {
    Task<T> Insert(T obj);
    Task<int> Update(T obj);
    Task<bool> Delete(int id);
    Task<IEnumerable<T>> Get();
  }
}
