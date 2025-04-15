namespace Domain.Ports
{
    public interface IMotoPublishToQueue
    {
        Task PublicaMenssagemParaFila(string mensagem);
    }
}