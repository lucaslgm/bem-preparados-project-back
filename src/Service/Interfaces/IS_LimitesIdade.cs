using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_LimitesIdade
  {
    Task<D_LimitesIdade> GetByAge(D_LimitesIdade dto);
    Task<D_LimitesIdade> GetMinMaxAgeByAffiliated(D_LimitesIdade dto);
    Task<IEnumerable<D_LimitesIdade>> GetByAffiliated(D_LimitesIdade dto);
  }
}
