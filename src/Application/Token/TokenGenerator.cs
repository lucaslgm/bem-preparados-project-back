using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Token
{
  public class TokenGenerator : ITokenGenerator
  {
    private readonly IConfiguration _configuration;

    public TokenGenerator(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    string ITokenGenerator.GenerateToken()
    {
      // => Irá manusear o token
      var tokenHandler = new JwtSecurityTokenHandler();

      // => Chave de criptografia para geração do token
      var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

      //    => Payload
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        //    => recebe os dados do usuário
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.Name, _configuration["Jwt:Login"]),
                    new Claim(ClaimTypes.Role, "User")
          }),

        //   => Tempo de validade
        Expires = DateTime.UtcNow.AddHours(int.Parse(_configuration["Jwt:HoursToExpire"])),

        //  => chave de criptografia
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      //  => Cria o Token
      var token = tokenHandler.CreateToken(tokenDescriptor);

      //  => Retorna o token assinado
      return tokenHandler.WriteToken(token);
    }
  }
}
