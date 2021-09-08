using System;
using System.Net;
using System.Threading.Tasks;
using Application.Models;
using Application.Utilities;
using AutoMapper;
using CrossCutting.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Service.Dtos;
using Service.Interfaces;

namespace Application.Controllers
{
  [ApiController]

  public class CadastroController : ControllerBase
  {
    private readonly IS_Cliente _clienteServico;
    private readonly IS_Proposta _propostaServico;
    private readonly IS_Cadastro _cadastroServico;
    private readonly IMapper _mapper;

    public CadastroController(IS_Cliente clienteServico, IS_Proposta propostaServico, IMapper mapper, IS_Cadastro cadastroServico)
    {
      _clienteServico = clienteServico;
      _propostaServico = propostaServico;
      _mapper = mapper;
      _cadastroServico = cadastroServico;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/form/get-by-client")]
    public async Task<IActionResult> GetCadastro([FromQuery] string cpf)
    {
      try
      {
        var cliente = await _clienteServico.GetByCpf(cpf);

        if (cliente == null)
        {
          return NotFound();
        }

        var proposta = await _propostaServico.GetByCliente(cpf);

        return Ok(new { cliente, proposta });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/form/get-financed-amount")]
    public async Task<IActionResult> GetValorFinanciado([FromQuery] double vlrSolicitado, int prazo)
    {
      double c = vlrSolicitado;
      int t = prazo;

      var vlrFinanciado = await _cadastroServico.CalculaValorFinanciado(c, t);

      return Ok(vlrFinanciado);
    }

    [HttpPost]
    [Authorize("Bearer")]
    [Route("/api/v1/form/insert")]
    public async Task<IActionResult> Insert([FromBody] M_InsercaoCadastro model)
    {
      try
      {
        var dto = _mapper.Map<D_InsercaoCadastro>(model);
        var cadastroInserido = await _cadastroServico.Insert(dto);

        return Ok(new M_Resultado
        {
          Message = "Cadastro inserido com sucesso!",
          Success = true,
          Data = cadastroInserido
        });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [Route("/api/v1/form/update/1")]
    public async Task<IActionResult> Update([FromBody] M_AtualizacaoCadastro model)
    {
      try
      {
        var dto = _mapper.Map<D_AtualizacaoCadastro>(model);

        var cadastroAtualizado = await _cadastroServico.Update(dto);

        return Ok(new M_Resultado
        {
          Message = "Cadastro atualizado com sucesso!",
          Success = true,
          Data = cadastroAtualizado
        });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }
  }
}
