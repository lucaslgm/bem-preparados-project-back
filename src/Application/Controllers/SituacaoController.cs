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
  public class SituacaoController : ControllerBase
  {
    private readonly IS_Situacao _situacaoServico;
    private readonly IMapper _mapper;

    public SituacaoController(IS_Situacao situacaoServico, IMapper mapper)
    {
      _situacaoServico = situacaoServico;
      _mapper = mapper;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/states/get-by-state")]
    public async Task<IActionResult> GetByState([FromQuery] M_Situacao model)
    {
      try
      {
        var dto = _mapper.Map<D_Situacao>(model);
        var situacao = await _situacaoServico.GetByState(dto);

        if (situacao == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhuma Situção foi encontrado com o dado informado.",
            Success = false,
            Data = situacao
          });


        return Ok(situacao);
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
    [Route("/api/v1/states/get-all")]
    public async Task<IActionResult> Get()
    {
      try
      {
        var allStates = await _situacaoServico.Get();
        return Ok(allStates);
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
