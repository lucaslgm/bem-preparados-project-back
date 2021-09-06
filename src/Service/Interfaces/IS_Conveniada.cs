using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Conveniada
  {
    Task<D_Conveniada> Insert(D_Conveniada dto);
    Task<IEnumerable<D_Conveniada>> Get();
    Task<D_Conveniada> GetByAffiliated(D_Conveniada dto);
    Task<int> Update(D_Conveniada dto);
    Task<bool> Delete(D_Conveniada dto);
  }
}
