using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace Domain.Tokens
{
  public class AccessConfigurations
  {
    public SecurityKey Key { get; set; }
    public SigningCredentials SigningCredentials { get; set; }

    public AccessConfigurations()
    {
      using (var provider = new RSACryptoServiceProvider(2048))
      {
        Key = new RsaSecurityKey(provider.ExportParameters(true));
      }

      SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
    }
  }
}
