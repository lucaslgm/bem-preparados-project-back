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
  public class S_Cliente : IS_Cliente
  {
    private readonly IMapper _mapper;
    private readonly IR_Cliente _clienteRepositorio;

    public S_Cliente(IMapper mapper, IR_Cliente clienteRepositorio)
    {
      _mapper = mapper;
      _clienteRepositorio = clienteRepositorio;
    }

    public async Task<D_Cliente> Insert(D_Cliente dto)
    {
      var clienteExiste = await _clienteRepositorio.GetByCpf(dto.Cpf);
      if (clienteExiste != null)
      {
        throw new DomainException("Já existe um usuário cadastrado para esse username.");
      }

      var entidade = _mapper.Map<E_Cliente>(dto);
      entidade.Validate();

      var clienteInserido = await _clienteRepositorio.Insert(entidade);

      return _mapper.Map<D_Cliente>(clienteInserido);
    }

    public async Task<IEnumerable<D_Cliente>> Get()
    {
      var lista = await _clienteRepositorio.Get();
      return _mapper.Map<IEnumerable<D_Cliente>>(lista);
    }

    public async Task<D_Cliente> GetByCpf(string cpf)
    {
      var entidade = await _clienteRepositorio.GetByCpf(cpf);
      return _mapper.Map<D_Cliente>(entidade);
    }

    public async Task<int> Update(D_Cliente dto)
    {
      var clienteExiste = await _clienteRepositorio.GetByCpf(dto.Cpf);

      if (clienteExiste == null)
      {
        throw new DomainException("Não existe nenhum cliente com o cpf informado!");
      }

      var entidade = _mapper.Map<E_Cliente>(dto);
      entidade.Validate();

      var ret = await _clienteRepositorio.Update(entidade);

      return ret;
    }
  }
}
