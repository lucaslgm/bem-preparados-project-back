using System.Collections.Generic;
using System.Threading.Tasks;
using Service.Dtos;

namespace Service.Interfaces
{
  public interface IS_Proposta
  {
    Task<D_Proposta> Insert(D_Proposta dto);
    Task<int> Update(D_Proposta dto);
    Task<IEnumerable<D_Proposta>> Get();
    Task<IEnumerable<D_Proposta>> GetByUsuario(string usuario);
    Task<IEnumerable<D_PropostaCompleta>> GetPropostasCompletas(string usuario);
    Task<D_PropostaCompleta> GetPropostaCompleta(string usuario, string cpf);
    Task<D_Proposta> GetByProposta(decimal proposta);
    Task<D_Proposta> GetByCliente(string cpf);
  }
}
