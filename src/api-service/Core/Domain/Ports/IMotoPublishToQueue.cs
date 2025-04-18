namespace Domain.Ports
{
    public interface IMotoPublishToQueue
    {
        Task PublicaMenssagemParaFilaAsync(string mensagem);
    }
}