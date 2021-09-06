using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using CrossCutting.Exceptions;
using Domain.Entities;
using Domain.Tokens;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Dtos;
using Service.Interfaces;

namespace Services
{
  public class S_Login : IS_Login
  {
    private readonly IMapper _mapper;
    private readonly IR_Login _loginRepository;
    private AccessConfigurations _accessConfig;
    private readonly IConfiguration _configuration;

    public S_Login(IMapper mapper,
                        IR_Login loginRepository,
                        AccessConfigurations accessConfig,
                        IConfiguration configuration)
    {
      _mapper = mapper;
      _loginRepository = loginRepository;
      _accessConfig = accessConfig;
      _configuration = configuration;
    }

    public async Task<object> ValidateLogin(D_Login usuario)
    {
      if (usuario == null)
      {
        throw new DomainException("Já existe um usuário cadastrato para esse username.");
      }

      var entidade = _mapper.Map<E_Login>(usuario);
      var ret = await _loginRepository.ValidateLogin(entidade);

      if (ret == null)
      {
        return new
        {
          authenticated = false,
          message = "Usuário e/ou senha inválidos",
        };
      }
      if (ret == "SENHA")
      {
        return new
        {
          authenticated = false,
          message = "Usuário e/ou senha inválidos",
        };
      }
      if (ret == "VALIDADE")
      {
        return new
        {
          authenticated = false,
          message = "Senha expirada!",
          data = ret
        };
      }

      ClaimsIdentity identity = new ClaimsIdentity(
          new GenericIdentity(usuario.Usuario),
          new[]
          {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Usuario),
          }
      );

      DateTime createDate = DateTime.UtcNow;
      DateTime expirationDate = createDate + TimeSpan.FromMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("Seconds")));

      var handler = new JwtSecurityTokenHandler();
      string token = CreateToken(identity, createDate, expirationDate, handler);
      return SuccessObject(createDate, expirationDate, token, ret, entidade);
    }
    private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
    {
      var securityToken = handler.CreateToken(new SecurityTokenDescriptor
      {
        Audience = Environment.GetEnvironmentVariable("Audience"),
        Issuer = Environment.GetEnvironmentVariable("Issuer"),
        SigningCredentials = _accessConfig.SigningCredentials,
        Subject = identity,
        NotBefore = createDate,
        Expires = expirationDate,
      });

      var token = handler.WriteToken(securityToken);
      return token;
    }

    private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, string ret, E_Login usuario)
    {
      return new
      {
        authenticated = true,
        create = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
        expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
        accessToken = token,
        name = ret.ToString().Trim(),
        message = "Usuário Logado com sucesso"
      };
    }
  }
}
