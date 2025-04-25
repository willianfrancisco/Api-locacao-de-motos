namespace Domain.Ports
{
    public interface ISerilogLogger
    {
        void LogInfo(string mensagem);
        void LogError(string mensagem);
    }
}