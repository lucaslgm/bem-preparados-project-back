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
  public class S_Proposta : IS_Proposta
  {
    private readonly IMapper _mapper;
    private readonly IR_Proposta _propostaRepositorio;
    private readonly IR_PropostaCompleta _propostaCompletaRepositorio;

    public S_Proposta(IMapper mapper, IR_Proposta propostaRepositorio, IR_PropostaCompleta propostaCompletaRepositorio)
    {
      _mapper = mapper;
      _propostaRepositorio = propostaRepositorio;
      _propostaCompletaRepositorio = propostaCompletaRepositorio;
    }

    public async Task<IEnumerable<D_Proposta>> Get()
    {
      var listaEntidades = await _propostaRepositorio.Get();
      return _mapper.Map<IEnumerable<D_Proposta>>(listaEntidades);
    }

    public async Task<D_Proposta> GetByCliente(string cpf)
    {
      var entidade = await _propostaRepositorio.GetByCliente(cpf);
      var dtoProposta = _mapper.Map<D_Proposta>(entidade);
      return dtoProposta;
    }

    public async Task<D_Proposta> GetByProposta(decimal proposta)
    {
      var entidade = await _propostaRepositorio.GetByProposta(proposta);
      return _mapper.Map<D_Proposta>(entidade);
    }

    public async Task<IEnumerable<D_Proposta>> GetByUsuario(string usuario)
    {
      var entidade = await _propostaRepositorio.GetByUsuario(usuario);
      return _mapper.Map<IEnumerable<D_Proposta>>(entidade);
    }

    public async Task<IEnumerable<D_PropostaCompleta>> GetPropostasCompletas(string usuario)
    {
      var entidade = await _propostaCompletaRepositorio.GetPropostasCompletas(usuario);
      var dto = _mapper.Map<IEnumerable<D_PropostaCompleta>>(entidade);
      return dto;
    }
    public async Task<D_PropostaCompleta> GetPropostaCompleta(string usuario, string cpf)
    {
      var entidade = await _propostaCompletaRepositorio.GetPropostaCompleta(usuario, cpf);
      var dto = _mapper.Map<D_PropostaCompleta>(entidade);
      return dto;
    }

    public async Task<D_Proposta> Insert(D_Proposta dto)
    {
      var propostaExiste = await _propostaRepositorio.GetByProposta(dto.Proposta);
      if (propostaExiste != null)
      {
        throw new DomainException("Já existe uma proposta cadastrada com esse número.");
      }

      var entidade = _mapper.Map<E_Proposta>(dto);
      var propostaCriada = await _propostaRepositorio.Insert(entidade);
      return _mapper.Map<D_Proposta>(propostaCriada);
    }

    public async Task<int> Update(D_Proposta dto)
    {
      var propostaExiste = await _propostaRepositorio.GetByProposta(dto.Proposta);

      if (propostaExiste == null)
      {
        throw new DomainException("Não existe nenhuma proposta cadastrada com esse número!");
      }

      var entidade = _mapper.Map<E_Proposta>(dto);
      entidade.Validate();

      var ret = await _propostaRepositorio.Update(entidade);

      return ret;
    }
  }
}
