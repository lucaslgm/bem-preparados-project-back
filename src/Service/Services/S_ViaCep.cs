using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Service.Dtos;
using Service.Interfaces;

namespace Services
{
  public class S_ViaCep : IS_ViaCep
  {
    private HttpClient cliente = new HttpClient();

    public S_ViaCep()
    {
    }

    public async Task<object> GetEndereco(string cep)
    {
      cliente.BaseAddress = new Uri("https://viacep.com.br/ws/");
      cliente.DefaultRequestHeaders.Accept.Clear();
      cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

      var ret = await cliente.GetAsync($"{cep}/json/");
      if (ret.IsSuccessStatusCode)
      {
        var dados = await ret.Content.ReadAsStringAsync();
        var dto = JsonConvert.DeserializeObject<D_ViaCep>(dados);
        return dto.Cep == null ? null : dto;
      }
      return null;
    }
  }
}
