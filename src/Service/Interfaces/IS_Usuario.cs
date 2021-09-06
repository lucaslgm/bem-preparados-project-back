using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Usuario
  {
    Task<D_Usuario> Insert(D_Usuario user);
    Task<int> Update(D_Usuario obj);
    Task<bool> Delete(int id);
    Task<D_Usuario> Get(int id);
    Task<IEnumerable<D_Usuario>> Get();
    Task<D_Usuario> GetByUsername(string username);
  }
}
