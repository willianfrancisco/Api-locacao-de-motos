using System.Threading.Tasks;
using Application.DTOs;
using Application.UseCases;
using Domain.Entities;
using Domain.Ports;
using Moq;
using Newtonsoft.Json;

namespace testes.Core.Application.UseCase
{
    public class MotoUseCaseTest
    {
        [Fact]
        public async Task AtualizaPlacaMotoAsync_Deve_Atualizar_Placa_Sucesso()
        {
            //Arrange
            var mockMotoPublish = new Mock<IMotoPublishToQueue>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var moto = new Moto(1, 2025, "Fazer 250", "PPOLT");
            var novaPlaca = new AtualizaPlacaMotoDto("APOLB");

            mockMotoRepository.Setup(mm => mm.RecuperarMotoPorIdAsync(1)).ReturnsAsync(moto);

            var useCase = new MotoUseCase(mockMotoPublish.Object, mockMotoRepository.Object, mockLogger.Object);

            //Act
            await useCase.AtualizaPlacaMotoAsync(1, novaPlaca);

            //Assert
            mockMotoRepository.Verify(mm => mm.AtualizaPlacaMotoAsync(1, novaPlaca.placa), Times.Once);
            mockLogger.Verify(l => l.LogInfo($"Placa atualizada com sucesso. Moto: {moto.Id}, Placa nova{novaPlaca.placa}"));
        }

        [Fact]
        public async Task DeletarMotoAsync_Deve_Deletar_Moto_Sucesso()
        {
            //Arrange
            var mockMotoPublish = new Mock<IMotoPublishToQueue>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var moto = new Moto(1, 2025, "Fazer 250", "PPOLT");

            mockMotoRepository.Setup(mm => mm.RecuperarMotoPorIdAsync(1)).ReturnsAsync(moto);
            var useCase = new MotoUseCase(mockMotoPublish.Object, mockMotoRepository.Object, mockLogger.Object);

            //Act
            await useCase.DeletarMotoAsync(1);

            //Assert
            mockMotoRepository.Verify(mm => mm.DeletarMotoAsync(1), Times.Once);
            mockLogger.Verify(l => l.LogInfo($"Moto deletada com sucesso, moto:1"));
        }

        [Fact]
        public async Task RecuperarMotoPeloIdAsync_Deve_Retornar_Moto_Pelo_Id_Sucesso()
        {
            //Arrange
            var mockMotoPublish = new Mock<IMotoPublishToQueue>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var moto = new Moto(1, 2025, "Fazer 250", "PPOLT");

            mockMotoRepository.Setup(mm => mm.RecuperarMotoPorIdAsync(1)).ReturnsAsync(moto);

            var useCase = new MotoUseCase(mockMotoPublish.Object, mockMotoRepository.Object, mockLogger.Object);

            //Act
            await useCase.RecuperarMotoPeloIdAsync(1);

            //Assert
            mockMotoRepository.Verify(mm => mm.RecuperarMotoPorIdAsync(1), Times.Once);
            mockLogger.Verify(l => l.LogInfo("Realizada consulta para recuperar moto por id:1"));
        }

        [Fact]
        public async Task RecuperarMotoPelaPlacaAsync_Deve_Retornar_Moto_Pela_Placa_Sucesso()
        {
            //Arrange
            var mockMotoPublish = new Mock<IMotoPublishToQueue>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var moto = new Moto(1, 2025, "Fazer 250", "PPOLT");

            mockMotoRepository.Setup(mm => mm.RecuperarMotoPelaPlacaAsync("PPOLT")).ReturnsAsync(moto);

            var useCase = new MotoUseCase(mockMotoPublish.Object, mockMotoRepository.Object, mockLogger.Object);

            //Act
            await useCase.RecuperarMotoPelaPlacaAsync("PPOLT");

            //Assert
            mockMotoRepository.Verify(mm => mm.RecuperarMotoPelaPlacaAsync("PPOLT"), Times.Once);
            mockLogger.Verify(l => l.LogInfo("Realizada consulta para recuperar moto pela placa:PPOLT."));
        }

        [Fact]
        public async Task RecuperarTodasMotosAsync_Deve_Retornar_Todas_Motos_Da_Base_Sucesso()
        {
            //Arrange
            var mockMotoPublish = new Mock<IMotoPublishToQueue>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var motos = new List<Moto> {
                new Moto(1, 2025, "Fazer 250", "PPOLT"),
                new Moto(2, 2025, "CG 160", "BPOAT")
                };

            mockMotoRepository.Setup(mm => mm.ListarTodasMotosCadastradasAsync()).ReturnsAsync(motos);

            var useCase = new MotoUseCase(mockMotoPublish.Object, mockMotoRepository.Object, mockLogger.Object);

            //Act
            await useCase.RecuperarTodasMotosAsync();

            //Assert
            mockMotoRepository.Verify(mm => mm.ListarTodasMotosCadastradasAsync(), Times.Once);
            mockLogger.Verify(l => l.LogInfo("Realizada consulta para recuperar todas as motos."));
        }

        [Fact]
        public async Task PublicaMenssagemParaFilaAsync_Deve_Publicar_Mensagem_Sucesso()
        {
            //Arrange
            var mockMotoPublish = new Mock<IMotoPublishToQueue>();
            var mockMotoRepository = new Mock<IMotoRepository>();
            var mockLogger = new Mock<ISerilogLogger>();

            var moto = new CriarNovaMotoDto(2025,"Fazer","BPOAT");
            var mensagem = JsonConvert.SerializeObject(moto);

            mockMotoPublish.Setup(mm => mm.PublicaMenssagemParaFilaAsync(mensagem));

            var useCase = new MotoUseCase(mockMotoPublish.Object, mockMotoRepository.Object, mockLogger.Object);
            //Act
            await useCase.PublicaMenssagemParaFilaAsync(moto);

            //Assert
            mockMotoPublish.Verify(q => q.PublicaMenssagemParaFilaAsync(mensagem), Times.Once);
            mockLogger.Verify(l => l.LogInfo("Mensagem publicada na fila do rabbit com sucesso."));
        }
    }
}