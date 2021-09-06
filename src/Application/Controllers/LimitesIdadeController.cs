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
  public class LimitesIdadeController : ControllerBase
  {
    private readonly IS_LimitesIdade _limitesIdadeService;
    private readonly IMapper _mapper;

    public LimitesIdadeController(IS_LimitesIdade limitesIdadeService, IMapper mapper)
    {
      _limitesIdadeService = limitesIdadeService;
      _mapper = mapper;
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/limitsAge/get-by-affiliated")]
    public async Task<IActionResult> GetByAffiliated([FromQuery] M_LimitesIdade model)
    {
      try
      {
        var dto = _mapper.Map<D_LimitesIdade>(model);
        var result = await _limitesIdadeService.GetByAffiliated(dto);

        if (result == null)
        {
          return Ok(new M_Resultado
          {
            Message = "Nenhum dado foi encontrado com o parametro informado.",
            Success = false,
            Data = result
          });
        }

        return Ok(result);
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
    [Route("/api/v1/limitsAge/get-by-age")]
    public async Task<IActionResult> GetByAge([FromQuery] M_LimitesIdade model)
    {
      try
      {
        var dto = _mapper.Map<D_LimitesIdade>(model);
        var result = await _limitesIdadeService.GetByAge(dto);

        if (result == null)
        {
          return Ok(new M_Resultado
          {
            Message = "Nenhum dado foi encontrado com os parametros informados.",
            Success = false,
            Data = result
          });
        }


        return Ok(result);
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
