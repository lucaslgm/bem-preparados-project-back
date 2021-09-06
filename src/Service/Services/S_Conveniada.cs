using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CrossCutting.Exceptions;
using Domain.Entities;
using Infrastructure.Interfaces;
using Service.Dtos;
using Service.Interfaces;

namespace Services
{
  public class S_Conveniada : IS_Conveniada
  {
    private readonly IMapper _mapper;
    private readonly IR_Conveniada _conveniadaRepositorio;

    public S_Conveniada(IMapper mapper, IR_Conveniada conveniadaRepositorio)
    {
      _mapper = mapper;
      _conveniadaRepositorio = conveniadaRepositorio;
    }
    public async Task<D_Conveniada> Insert(D_Conveniada dto)
    {
      var conveniadaExiste = await _conveniadaRepositorio.GetByAffiliated(dto.Conveniada);
      if (conveniadaExiste != null)
      {
        throw new DomainException("Já existe um usuário cadastrato para esse username.");
      }

      var entidade = _mapper.Map<E_Conveniada>(dto);
      var dtoCriado = await _conveniadaRepositorio.Insert(entidade);

      return _mapper.Map<D_Conveniada>(dtoCriado);
    }
    public async Task<IEnumerable<D_Conveniada>> Get()
    {
      var listaEntidades = await _conveniadaRepositorio.Get();
      return _mapper.Map<IEnumerable<D_Conveniada>>(listaEntidades);
    }
    public async Task<D_Conveniada> GetByAffiliated(D_Conveniada dto)
    {
      var entidade = await _conveniadaRepositorio.GetByAffiliated(dto.Conveniada);
      return _mapper.Map<D_Conveniada>(entidade);
    }
    public Task<int> Update(D_Conveniada dto)
    {
      throw new System.NotImplementedException();
    }
    public Task<bool> Delete(D_Conveniada dto)
    {
      throw new System.NotImplementedException();
    }
  }
}
