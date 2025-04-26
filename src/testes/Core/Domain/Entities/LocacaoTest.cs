using Domain.Entities;
using Xunit;

namespace testes.Core.Domain.Entities
{
    public class LocacaoTest
    {
        [Fact]
        public void TesteCalculaValorTotalLocacao_Deve_Calcular_ValorTotal_Sucesso()
        {
            //Arrange
            var locacao = new Locacao(1,1,DateTime.Now,7);
            //Act
            var result = locacao.ValorTotalLocacao;
            //Assert
            Assert.Equal(210,result);
        }
    }
}