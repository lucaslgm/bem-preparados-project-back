using System;
using System.Threading.Tasks;
using AutoMapper;
using Infrastructure.Interfaces;
using Service.Interfaces;

namespace Services
{
  public class S_Cadastro : IS_Cadastro
  {
    private readonly IMapper _mapper;
    private readonly IR_Parametros _parametrosRepositorio;

    public S_Cadastro(IMapper mapper, IR_Parametros parametrosRepositorio)
    {
      _mapper = mapper;
      _parametrosRepositorio = parametrosRepositorio;
    }

    public async Task<decimal> CalculaValorFinanciado(double vlrSolicitado, int prazo)
    {
      double i = (await _parametrosRepositorio.GetTaxaJuros()) / 100;
      // double i = 0.01;
      double c = vlrSolicitado;
      int t = prazo;
      double m;
      m = c * Math.Pow((1 + i), t);
      return (decimal)m;

      // return getResult(vlrSolicitado, prazo, i);

    }

    public decimal getResult(double vlrSolicitado, int prazo, double txjrs)
    {
      double i = txjrs;
      double c = vlrSolicitado;
      int t = prazo;
      double m;
      m = c * Math.Pow((1 + i), t);
      return (decimal)m;
    }
  }
}
