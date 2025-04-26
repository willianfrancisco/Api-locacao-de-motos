using System.Threading.Tasks;
using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using Domain.Enums;
using Domain.Ports;
using Moq;

namespace testes.Core.Application.UseCase
{
    public class EntregadorUseCaseTest
    {        
        [Fact]
        public async Task AtualizarFotoCNHentregadorAsync_Deve_Atualizar_Foto_CNH_Sucesso()
        {
            // Arrange
            var mockRepo = new Mock<IEntregadorRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var entregador = new Entregador(
                1,
                "George",
                "56344795000100",
                new DateTime(1953,12,09),
                "000000",
                ETipoCNH.A,
                "ZHNqYWFkdm1rc2ZkZGRkZGRkZGRkZG5mYmxrbXZBRg");

            var novaFoto = new AtualizaFotoCnh("ZHNqYWFkdm1rc2ZkZGRkZGRkZGRkZG5mYmxrbXZBRg==");

            mockRepo.Setup(r => r.RecuperaEntregadorPeloIdAsync(1)).ReturnsAsync(entregador);

            var useCase = new EntregadorUseCase(mockRepo.Object, mockLogger.Object);

            // Act
            await useCase.AtualizarFotoCNHentregadorAsync(1,novaFoto);

            // Assert
            mockRepo.Verify(r => r.AtualizaFotoCNHEntregadorAsync(entregador.CNPJ ?? It.IsAny<string>(), novaFoto.novaFoto), Times.Once);
            mockLogger.Verify(l => l.LogInfo($"Foto cnh atualizada com sucesso, entregador: {entregador.Nome}"));
        }

        [Fact]
        public async Task CadastrarEntregadorAsync_Deve_Cadastrar_Entregador_Sucesso()
        {
            //Arrange
            var mockRepo = new Mock<IEntregadorRepository>();
            var mockLoger = new Mock<ISerilogLogger>();

            var entregadorNovo = new CriarNovoEntregador(
                "George",
                "56344795000100",
                new DateTime(1953,12,09),
                "000000",
                ETipoCNHDto.A,
                "ZHNqYWFkdm1rc2ZkZGRkZGRkZGRkZG5mYmxrbXZBRg");
            
            mockRepo.Setup(r => r.RecuperaEntregadorPeloCNPJAsync(entregadorNovo.CNPJ ?? It.IsAny<string>())).ReturnsAsync((Entregador)null);
            mockRepo.Setup(r => r.RecuperaEntregadorPelaCNHAsync(entregadorNovo.NumeroCNH ?? It.IsAny<string>())).ReturnsAsync((Entregador)null);

            var useCase = new EntregadorUseCase(mockRepo.Object, mockLoger.Object);

            //Act

            await useCase.CadastrarEntregadorAsync(entregadorNovo);

            //Assert
            mockRepo.Verify(r => r.CadastrarEntregadorAsync(It.IsAny<Entregador>()), Times.Once);
            mockLoger.Verify(l => l.LogInfo($"Entregador cadastrado com sucesso,{entregadorNovo.Nome}"));
        }
    }
}