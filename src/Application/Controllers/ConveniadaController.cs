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

  public class ConveniadaController : ControllerBase
  {
    private readonly IS_Conveniada _conveniadaServico;
    private readonly IMapper _mapper;

    public ConveniadaController(IS_Conveniada conveniadaServico, IMapper mapper)
    {
      _conveniadaServico = conveniadaServico;
      _mapper = mapper;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/affiliated/get-by-affiliated")]
    public async Task<IActionResult> GetByState([FromQuery] M_Conveniada model)
    {
      try
      {
        var dto = _mapper.Map<D_Conveniada>(model);
        var retorno = await _conveniadaServico.GetByAffiliated(dto);

        if (retorno == null)
        {
          return Ok(new M_Resultado
          {
            Message = "Nenhum dado foi encontrado com o parametro informado.",
            Success = false,
            Data = retorno
          });
        }


        return Ok(retorno);
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message));
      }
      catch (Exception e)
      {
        // return StatusCode(500, Responses.ApplicationErrorMessage());
        return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/affiliated/get-all")]
    public async Task<IActionResult> Get()
    {
      try
      {
        var listaConveniadas = await _conveniadaServico.Get();
        return Ok(listaConveniadas);
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
