namespace Domain.Tokens
{
  public class TokenConfig
  {
    public string Audience { get; set; }
    public string Issuer { get; set; }
    public int Seconds { get; set; }
  }
}
