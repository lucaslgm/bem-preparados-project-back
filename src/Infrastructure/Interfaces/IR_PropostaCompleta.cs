using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Interfaces
{
  public interface IR_PropostaCompleta
  {
    Task<IEnumerable<E_PropostaCompleta>> GetPropostasCompletas(string usuario);
    Task<E_PropostaCompleta> GetPropostaCompleta(string usuario, string cpf);
  }
}
