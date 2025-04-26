
using System.Threading.Tasks;
using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;

namespace testes.Core.Application.UseCase
{
    public class LocacaoUseCaseTest
    {
        [Fact]
        public async Task AtualizaDataDevolucaoAsync_Deve_Atualizar_Data_Sucesso()
        {
            //Arrange
            var mockLocacaoRepository = new Mock<ILocacaoRepository>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockEntregadorRepository = new Mock<IEntregadorRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var locacao = new Locacao(
                1,
                1,
                1,
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                7);

            var novaData = new AtualizaLocacaoDto(new DateTime(2025, 04, 25));

            mockLocacaoRepository.Setup(lr => lr.RecuperaLocacaoPorIdAsync(1)).ReturnsAsync(locacao);

            var useCase = new LocacaoUseCase(mockLocacaoRepository.Object, mockMotoRepository.Object, mockEntregadorRepository.Object, mockLogger.Object);

            //Act
            await useCase.AtualizaDataDevolucaoAsync(1, novaData);

            //Assert
            mockLocacaoRepository.Verify(lr => lr.AtualizaDataDevolucaoAsync(1, It.IsAny<DateTime>()), Times.Once);
            mockLogger.Verify(l => l.LogInfo($"Data devolução atualizada com sucesso para a locacao {locacao.Id}."));
        }

        [Fact]
        public async Task CadastraLocacaoAsync_Deve_Cadastrar_Nova_Locacao_Sucesso()
        {
            //Arrange
            var mockLocacaoRepository = new Mock<ILocacaoRepository>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockEntregadorRepository = new Mock<IEntregadorRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var novaLocacao = new CriaNovaLocacaoDto(1, 1, new DateTime(2025, 04, 25), 7);
            var entregador = new Entregador(
                "George",
                "56344795000100",
                new DateTime(1953, 12, 09),
                "000000",
                ETipoCNH.A,
                "ZHNqYWFkdm1rc2ZkZGRkZGRkZGRkZG5mYmxrbXZBRg");

            var moto = new Moto(1, 2025, "Fazer 250", "PFG0325");

            mockEntregadorRepository.Setup(me => me.RecuperaEntregadorPeloIdAsync(1)).ReturnsAsync(entregador);
            mockMotoRepository.Setup(mm => mm.RecuperarMotoPorIdAsync(1)).ReturnsAsync(moto);

            var useCase = new LocacaoUseCase(mockLocacaoRepository.Object, mockMotoRepository.Object, mockEntregadorRepository.Object, mockLogger.Object);

            //Act
            await useCase.CadastraLocacaoAsync(novaLocacao);

            //Assert
            mockLocacaoRepository.Verify(ml => ml.CadastrarLocacaoAsync(It.IsAny<Locacao>()), Times.Once);
            mockLogger.Verify(l => l.LogInfo("Nova locacao cadastrada com sucesso."));
        }

        [Fact]
        public async Task RecuperaLocacaoPorIdAsync_Deve_Retornar_Uma_Locacao()
        {
            //Arrange
            var mockLocacaoRepository = new Mock<ILocacaoRepository>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockEntregadorRepository = new Mock<IEntregadorRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var locacao = new Locacao(
                1,
                1,
                1,
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                It.IsAny<DateTime>(),
                7
            );
            
            mockLocacaoRepository.Setup(ml => ml.RecuperaLocacaoPorIdAsync(1)).ReturnsAsync(locacao);

            var useCase = new LocacaoUseCase(mockLocacaoRepository.Object, mockMotoRepository.Object, mockEntregadorRepository.Object, mockLogger.Object);

            //Act
            await useCase.RecuperaLocacaoPorIdAsync(1);

            //Assert
            mockLocacaoRepository.Verify(ml => ml.RecuperaLocacaoPorIdAsync(1), Times.Once);
            mockLogger.Verify(l => l.LogInfo("Realizada consultada para locacao:1"));
        }
    }
}