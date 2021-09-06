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
  public class PropostaController : ControllerBase
  {
    private readonly IS_Proposta _propostaServico;
    private readonly IMapper _mapper;

    public PropostaController(IS_Proposta propostaServico, IMapper mapper)
    {
      _propostaServico = propostaServico;
      _mapper = mapper;
    }

    [HttpPost]
    // [Authorize("Bearer")]
    [Route("/api/v1/proposal/insert")]
    public async Task<IActionResult> Insert([FromBody] M_InsercaoProposta model)
    {
      try
      {
        var dto = _mapper.Map<D_Proposta>(model);

        var retorno = await _propostaServico.Insert(dto);

        return Ok(new M_Resultado
        {
          Message = "Proposta inserido com sucesso!",
          Success = true,
          Data = retorno
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
    [Route("/api/v1/proposal/update")]
    public async Task<IActionResult> Update([FromBody] M_AtualizacaoProposta model)
    {
      try
      {
        var dto = _mapper.Map<D_Proposta>(model);

        var retorno = await _propostaServico.Update(dto);

        return Ok(new M_Resultado
        {
          Message = "Proposta atualizada com sucesso!",
          Success = true,
          Data = retorno
        });
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message, e.Errors));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/proposal/get-all")]
    public async Task<IActionResult> Get()
    {
      try
      {
        var lista = await _propostaServico.Get();

        return Ok(new M_Resultado
        {
          Message = "Propostas encontradas com sucesso!",
          Success = true,
          Data = lista
        });
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/proposal/get-by-user")]
    public async Task<IActionResult> GetByUser([FromQuery] string user)
    {
      try
      {
        var lista = await _propostaServico.GetByUsuario(user);

        if (lista == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhuma proposta foi encontrado para o usuário informado.",
            Success = false,
            Data = lista
          });


        return Ok(new M_Resultado
        {
          Message = "Usuário encontrado com sucesso!",
          Success = true,
          Data = lista
        });
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/proposal/get-full-by-user")]
    public async Task<IActionResult> GetFullByUser([FromQuery] string user)
    {
      try
      {
        var lista = await _propostaServico.GetPropostasCompletas(user);

        if (lista == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhuma proposta foi encontrado para o usuário informado.",
            Success = false,
            Data = lista
          });


        return Ok(lista);
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }
    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/proposal/get-full-by-client")]
    public async Task<IActionResult> GetFullByClient([FromQuery] string user, string cpf)
    {
      try
      {
        var prop = await _propostaServico.GetPropostaCompleta(user, cpf);

        if (prop == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhuma proposta foi encontrado para o usuário informado.",
            Success = false,
            Data = prop
          });


        return Ok(new M_Resultado
        {
          Message = "Usuário encontrado com sucesso!",
          Success = true,
          Data = prop
        });
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/proposal/get-by-proposal")]
    public async Task<IActionResult> GetByProposal([FromQuery] decimal proposal)
    {
      try
      {
        var lista = await _propostaServico.GetByProposta(proposal);

        if (lista == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhuma proposta foi encontrado para o usuário informado.",
            Success = false,
            Data = lista
          });


        return Ok(new M_Resultado
        {
          Message = "Usuário encontrado com sucesso!",
          Success = true,
          Data = lista
        });
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/proposal/get-by-client")]
    public async Task<IActionResult> GetByClient([FromQuery] string cpf)
    {
      try
      {
        var lista = await _propostaServico.GetByCliente(cpf);

        if (lista == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhuma proposta foi encontrado para o usuário informado.",
            Success = false,
            Data = lista
          });


        return Ok(new M_Resultado
        {
          Message = "Usuário encontrado com sucesso!",
          Success = true,
          Data = lista
        });
      }
      catch (DomainException e)
      {
        return BadRequest(Responses.DomainErrorMessage(e.Message));
      }
      catch (Exception e)
      {
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }
  }
}
