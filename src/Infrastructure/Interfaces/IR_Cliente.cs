using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_Cliente : IBaseRepository<E_Cliente>
  {
    Task<E_Cliente> GetByCpf(string cpf);
  }
}
