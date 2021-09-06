using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_LimitesIdade
  {
    Task<E_LimitesIdade> GetByAge(string conveniada, short idade);
    Task<E_LimitesIdade> GetMinMaxAgeByAffiliated(string conveniada);
    Task<IEnumerable<E_LimitesIdade>> GetByAffiliated(string conveniada);
  }
}
