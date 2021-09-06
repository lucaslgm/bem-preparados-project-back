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
  public class UsuarioController : ControllerBase
  {
    private readonly IS_Usuario _usuarioServico;
    private readonly IMapper _mapper;

    public UsuarioController(IS_Usuario usuarioServico, IMapper mapper)
    {
      _usuarioServico = usuarioServico;
      _mapper = mapper;
    }

    [HttpPost]
    [Authorize("Bearer")]
    [Route("/api/v1/users/insert")]
    public async Task<IActionResult> Insert([FromBody] M_InsercaoUsuario model)
    {
      try
      {
        var dto = _mapper.Map<D_Usuario>(model);

        var usuarioCriado = await _usuarioServico.Insert(dto);

        return Ok(new M_Resultado
        {
          Message = "Usuário inserido com sucesso!",
          Success = true,
          Data = usuarioCriado
        });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
      }
      catch (Exception)
      {
        return StatusCode(500, Responses.ApplicationErrorMessage());
      }
    }

    [HttpPut]
    [Authorize("Bearer")]
    [Route("/api/v1/users/update")]
    public async Task<IActionResult> Update([FromBody] M_AtualizacaoUsuario model)
    {
      try
      {
        var dto = _mapper.Map<D_Usuario>(model);

        var usuarioAtualizado = await _usuarioServico.Update(dto);

        return Ok(new M_Resultado
        {
          Message = "Usuário atualizado com sucesso!",
          Success = true,
          Data = usuarioAtualizado
        });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
      }
      catch (Exception ex)
      {
        return StatusCode(500, ex);
      }
    }

    [HttpDelete]
    [Authorize("Bearer")]
    [Route("/api/v1/users/delete/{id}")]
    public async Task<IActionResult> Remove(int id)
    {
      try
      {
        await _usuarioServico.Delete(id);

        return Ok(new M_Resultado
        {
          Message = "Usuário removido com sucesso!",
          Success = true,
          Data = null
        });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message));
      }
      catch (Exception)
      {
        return StatusCode(500, Responses.ApplicationErrorMessage());
      }
    }

    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/users/get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        var usuario = await _usuarioServico.Get(id);

        if (usuario == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhum usuário foi encontrado com o ID informado.",
            Success = true,
            Data = usuario
          });

        return Ok(new M_Resultado
        {
          Message = "Usuário encontrado com sucesso!",
          Success = true,
          Data = usuario
        });
      }
      catch (DomainException ex)
      {
        return BadRequest(Responses.DomainErrorMessage(ex.Message));
      }
      catch (Exception)
      {
        return StatusCode(500, Responses.ApplicationErrorMessage());
      }
    }


    [HttpGet]
    [Authorize("Bearer")]
    [Route("/api/v1/users/get-all")]
    public async Task<IActionResult> Get()
    {
      try
      {
        var lista = await _usuarioServico.Get();

        return Ok(new M_Resultado
        {
          Message = "Usuários encontrados com sucesso!",
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
    [Authorize("Bearer")]
    [Route("/api/v1/users/get-by-username")]
    public async Task<IActionResult> GetByUsername([FromQuery] string username)
    {
      try
      {
        var usuario = await _usuarioServico.GetByUsername(username);

        if (usuario == null)
          return Ok(new M_Resultado
          {
            Message = "Nenhum usuário foi encontrado com o email informado.",
            Success = false,
            Data = usuario
          });


        return Ok(new M_Resultado
        {
          Message = "Usuário encontrado com sucesso!",
          Success = true,
          Data = usuario
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
