using System;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Moq;
using Service.Interfaces;
using Services;
using Xunit;

namespace Test
{
  public class CadastroServiceTest
  {
    private Mock<IR_Parametros> _repoMock;
    private Mock<IS_Cadastro> _serviceMock;
    private IS_Cadastro _service;

    [Fact(DisplayName = "Calcula o valor financiado SUCESSO")]
    public void CalculaValorFinanciadoSucessoAsync()
    {
      double vlr = 1234.56;
      int prazo = 12;
      decimal esperado = Convert.ToDecimal(1391.1331091997245);

      _serviceMock = new Mock<IS_Cadastro>();
      _serviceMock.Setup(s => s.CalculaValorFinanciado(It.IsAny<double>(), It.IsAny<int>())).Returns(It.IsAny<Task<decimal>>());
      _repoMock = new Mock<IR_Parametros>();
      _repoMock.Setup(r => r.GetTaxaJuros()).Returns(Task.FromResult(1.0));

      _service = new S_Cadastro(null, _repoMock.Object);

      var resultado = _service.CalculaValorFinanciado(vlr, prazo);
      Assert.True(Convert.ToDecimal(resultado.Result) == esperado);
      Assert.Equal(Convert.ToDecimal(resultado.Result), esperado);
    }
  }
}
