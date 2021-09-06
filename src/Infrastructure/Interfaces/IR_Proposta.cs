using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_Proposta : IBaseRepository<E_Proposta>
  {
    Task<E_Proposta> GetByProposta(decimal proposta);
    Task<E_Proposta> GetByCliente(string cpf);
    Task<IEnumerable<E_Proposta>> GetByUsuario(string usuario);
  }
}
