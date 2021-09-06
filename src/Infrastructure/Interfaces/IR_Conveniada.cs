using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_Conveniada : IBaseRepository<E_Conveniada>
  {
    Task<E_Conveniada> GetByAffiliated(string conveniada);
  }
}
