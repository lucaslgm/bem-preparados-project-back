using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context
{
  public class RepositoryConfiguration
  {
    public IConfiguration _configuration;

    public RepositoryConfiguration(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public string GetConnection()
    {
      return _configuration.GetSection("ConnectionStrings").GetSection("Default").Value;
    }
  }
}
