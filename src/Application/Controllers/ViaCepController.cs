using System;
using System.Net;
using System.Threading.Tasks;
using Application.Models;
using Application.Utilities;
using CrossCutting.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace Application.Controllers
{
  [ApiController]

  public class ViaCepController : ControllerBase
  {
    private readonly IS_ViaCep _viacepServico;

    public ViaCepController(IS_ViaCep viacepServico)
    {
      _viacepServico = viacepServico;
    }

    [HttpGet]
    [Authorize("Bearer")]

    [Route("/api/v1/viacep/get/{cep}")]
    public async Task<IActionResult> Get(string cep)
    {
      try
      {
        var endereco = await _viacepServico.GetEndereco(cep);

        return endereco == null ? NotFound() : Ok(endereco);
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
