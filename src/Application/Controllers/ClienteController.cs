using System;
using System.Net;
using System.Threading.Tasks;
using Application.Models;
using Application.Utilities;
using AutoMapper;
using CrossCutting.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Dtos;
using Service.Interfaces;

namespace Application.Controllers
{
  [ApiController]
  public class ClienteController : ControllerBase
  {
    private readonly IS_Cliente _clienteServico;
    private readonly IMapper _mapper;

    public ClienteController(IS_Cliente clienteServico, IMapper mapper)
    {
      _clienteServico = clienteServico;
      _mapper = mapper;
    }

    [HttpPost]
    [Authorize("Bearer")]
    [Route("/api/v1/clients/insert")]
    public async Task<IActionResult> Insert([FromBody] M_InsercaoCliente model)
    {
      try
      {
        var dto = _mapper.Map<D_Cliente>(model);

        var clienteInserido = await _clienteServico.Insert(dto);

        return Ok(new M_Resultado
        {
          Message = "Cliente inserido com sucesso!",
          Success = true,
          Data = clienteInserido
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
    [Route("/api/v1/clients/update")]
    public async Task<IActionResult> Update([FromBody] M_AtualizacaoCliente model)
    {
      try
      {
        var dto = _mapper.Map<D_Cliente>(model);

        var clienteAtualizado = await _clienteServico.Update(dto);

        return Ok(new M_Resultado
        {
          Message = "Cliente atualizado com sucesso!",
          Success = true,
          Data = clienteAtualizado
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

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/clients/get-all")]
    public async Task<IActionResult> Get()
    {
      try
      {
        var lista = await _clienteServico.Get();

        return Ok(new M_Resultado
        {
          Message = "Clientes encontrados com sucesso!",
          Success = true,
          Data = lista
        });
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
    // [Authorize("Bearer")]
    [Route("/api/v1/clients/get-by-client")]
    public async Task<IActionResult> GetByCliente([FromQuery] string cpf)
    {
      try
      {
        var cliente = await _clienteServico.GetByCpf(cpf);

        if (cliente == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhum cliente foi encontrado com o cpf informado.",
            Success = false,
            Data = cliente
          });


        return Ok(new M_Resultado
        {
          Message = "Cliente encontrado com sucesso!",
          Success = true,
          Data = cliente
        });
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
  }
}
