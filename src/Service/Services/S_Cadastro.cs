using System;
using System.Threading.Tasks;
using AutoMapper;
using CrossCutting.Exceptions;
using Domain.Entities;
using Infrastructure.Interfaces;
using MassTransit;
using Service.Dtos;
using Service.Interfaces;

namespace Services
{
  public class S_Cadastro : IS_Cadastro
  {
    private readonly IMapper _mapper;
    private readonly IR_Parametros _parametrosRepositorio;
    private readonly IR_Cliente _clienteRepositorio;
    private readonly IR_Cadastro _cadastroRepositorio;
    private readonly IR_Proposta _propostaRepositorio;
    private readonly IBusControl _busControl;

    public S_Cadastro(IMapper mapper, IR_Parametros parametrosRepositorio, IR_Cliente clienteRepositorio, IR_Cadastro cadastroRepositorio, IR_Proposta propostaRepositorio, IBusControl busControl)
    {
      _mapper = mapper;
      _parametrosRepositorio = parametrosRepositorio;
      _clienteRepositorio = clienteRepositorio;
      _cadastroRepositorio = cadastroRepositorio;
      _propostaRepositorio = propostaRepositorio;
      _busControl = busControl;
    }

    public async Task<decimal> CalculaValorFinanciado(double vlrSolicitado, int prazo)
    {
      double i = (await _parametrosRepositorio.GetTaxaJuros()) / 100;
      // double i = 0.01;
      double c = vlrSolicitado;
      int t = prazo;
      double m;
      m = c * Math.Pow((1 + i), t);
      return (decimal)m;

      // return getResult(vlrSolicitado, prazo, i);

    }

    public async void SendtoQueueAsync(D_PropostaFila proposta)
    {
      await _busControl.Publish<D_PropostaFila>(proposta);
    }

    public decimal getResult(double vlrSolicitado, int prazo, double txjrs)
    {
      double i = txjrs;
      double c = vlrSolicitado;
      int t = prazo;
      double m;
      m = c * Math.Pow((1 + i), t);
      return (decimal)m;
    }

    public async Task<D_InsercaoCadastro> Insert(D_InsercaoCadastro dto)
    {

      #region Checar existencia
      var clienteExiste = await _clienteRepositorio.GetByCpf(dto.Cpf);
      if (clienteExiste != null)
      {
        throw new DomainException("Já existe um cliente cadastrado com esse cpf.");
      }

      var propostaExiste = await _propostaRepositorio.GetByCliente(dto.Cpf);
      if (propostaExiste != null)
      {
        throw new DomainException("Já existe uma proposta cadastrada com esse número.");
      }
      #endregion


      var entidade = _mapper.Map<E_ClienteProposta>(dto);
      entidade.Validate();

      var clientePropostaInseridos = await _cadastroRepositorio.Insert(entidade);

      var numProposta = await _propostaRepositorio.GetNumeroProposta(clientePropostaInseridos.Id_treina_proposta);
      dto.Proposta = numProposta;

      var dtoFila = _mapper.Map<D_PropostaFila>(dto);
      SendtoQueueAsync(dtoFila);

      return _mapper.Map<D_InsercaoCadastro>(clientePropostaInseridos);
    }

    public async Task<int> Update(D_AtualizacaoCadastro dto)
    {
      #region Checar existencia
      var clienteExiste = await _clienteRepositorio.GetByCpf(dto.Cpf);
      if (clienteExiste == null)
      {
        throw new DomainException("Não existe nenhum cliente com o cpf informado!");
      }

      var propostaExiste = await _propostaRepositorio.GetByProposta(dto.Proposta);
      if (propostaExiste == null)
      {
        throw new DomainException("Não existe nenhuma proposta cadastrada com esse número!");
      }
      #endregion

      var entidade = _mapper.Map<E_ClienteProposta>(dto);
      entidade.Validate();

      var dtoFila = _mapper.Map<D_PropostaFila>(dto);
      SendtoQueueAsync(dtoFila);

      var ret = await _cadastroRepositorio.Update(entidade);

      return ret;
    }
  }
}
