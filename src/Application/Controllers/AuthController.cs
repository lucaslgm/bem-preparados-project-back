using System;
using System.Net;
using System.Threading.Tasks;
using Application.Models;
using Application.Token;
using Application.Utilities;
using AutoMapper;
using CrossCutting.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Dtos;
using Service.Interfaces;

namespace Application.Controllers
{
  [ApiController]
  public class AuthController : ControllerBase
  {
    private readonly IConfiguration _configuration;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IS_Login _loginServico;
    private readonly IMapper _mapper;

    public AuthController(IConfiguration configuration, ITokenGenerator tokenGenerator, IMapper mapper, IS_Login loginServico)
    {
      _configuration = configuration;
      _tokenGenerator = tokenGenerator;
      _loginServico = loginServico;
      _mapper = mapper;

    }
    [AllowAnonymous]
    [HttpPost]
    [Route("/api/v1/auth/login")]
    public async Task<Object> ValidateLogin([FromBody] M_Login model)
    {
      try
      {
        D_Login dto = _mapper.Map<D_Login>(model);

        var resultado = await _loginServico.ValidateLogin(dto);

        return resultado;

        #region old
        /*
                ResultModel resultModel = null;

                if (result == 10)
                {
                  resultModel = new ResultModel
                  {
                    Message = "Usuário valido!",
                    Success = true,
                    Data = result
                          };
                }
                else if (result == 0 || result == 1)
                {
                  resultModel = new ResultModel
                  {
                    Message = "Usuário e/ou senha incorretos!",
                    Success = false,
                    Data = result
                  };
                }
                else if (result == 2)
                {
                  resultModel = new ResultModel
                  {
                    Message = "Senha expirada!",
                    Success = false,
                    Data = result
                  };
                };

                return Ok(resultModel);
        */
        #endregion
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

    #region Validate Old
    // [HttpPost]
    // [Route("/api/v1/auth/login")]
    // public IActionResult Login([FromBody] LoginModel login)
    // {
    //   try
    //   {
    //     var tokenLogin = _configuration["Jwt:Usuario"];
    //     var tokenPassword = _configuration["Jwt:Senha"];

    //     if (login.Usuario == tokenLogin && loginModel.Senha == tokenPassword)
    //     {
    //       return Ok(new ResultModel
    //       {
    //         Message = "Usuário autenticado com sucesso!",
    //         Success = true,
    //         Data = new
    //         {
    //           Token = _tokenGenerator.GenerateToken(),
    //           TokenExpires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"]))
    //         }
    //       });
    //     }
    //     else
    //     {
    //       return StatusCode(401, Responses.UnauthorizedErrorMessage());
    //     }
    //   }
    //   catch (Exception)
    //   {
    //     return StatusCode(500, Responses.ApplicationErrorMessage());
    //   }
    // }
    #endregion
  }
}
