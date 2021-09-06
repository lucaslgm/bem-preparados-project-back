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
  public class S_Situacao : IS_Situacao
  {
    private readonly IMapper _mapper;
    private readonly IR_Situacao _situacaoRepositorio;

    public S_Situacao(IMapper mapper, IR_Situacao situacaoRepositorio)
    {
      _mapper = mapper;
      _situacaoRepositorio = situacaoRepositorio;
    }

    public async Task<D_Situacao> Insert(D_Situacao dto)
    {
      var stateExists = await _situacaoRepositorio.GetByState(dto.Situacao);
      if (stateExists != null)
      {
        throw new DomainException("Já existe um usuário cadastrato para esse username.");
      }

      var stateEntity = _mapper.Map<E_Situacao>(dto);
      var stateCreated = await _situacaoRepositorio.Insert(stateEntity);

      return _mapper.Map<D_Situacao>(stateCreated);
    }

    public async Task<D_Situacao> Get(D_Situacao dto)
    {
      var entity = await _situacaoRepositorio.Get(dto.Situacao);
      return _mapper.Map<D_Situacao>(entity);
    }

    public async Task<IEnumerable<D_Situacao>> Get()
    {
      var listEntity = await _situacaoRepositorio.Get();
      return _mapper.Map<IEnumerable<D_Situacao>>(listEntity);
    }

    public async Task<D_Situacao> GetByState(D_Situacao dto)
    {
      var entity = await _situacaoRepositorio.GetByState(dto.Situacao);
      return _mapper.Map<D_Situacao>(entity);
    }

    public Task<int> Update(D_Situacao dto)
    {
      throw new System.NotImplementedException();
    }

    public Task<bool> Delete(D_Situacao dto)
    {
      throw new System.NotImplementedException();
    }
  }
}
