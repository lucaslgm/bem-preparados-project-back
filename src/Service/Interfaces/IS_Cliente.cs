using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Cliente
  {
    Task<D_Cliente> Insert(D_Cliente dto);

    Task<IEnumerable<D_Cliente>> Get();

    Task<D_Cliente> GetByCpf(string cpf);

    Task<int> Update(D_Cliente obj);
  }
}
