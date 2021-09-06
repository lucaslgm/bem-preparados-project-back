using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Situacao
  {
    Task<D_Situacao> Insert(D_Situacao dto);
    Task<int> Update(D_Situacao dto);
    Task<bool> Delete(D_Situacao dto);
    Task<D_Situacao> Get(D_Situacao dto);
    Task<IEnumerable<D_Situacao>> Get();
    Task<D_Situacao> GetByState(D_Situacao dto);
  }
}
