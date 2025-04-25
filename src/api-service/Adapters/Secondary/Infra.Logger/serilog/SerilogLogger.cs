using Domain.Ports;
using Serilog;

namespace Infra.Logger.serilog
{
    public class SerilogLogger(ILogger _logger) : ISerilogLogger
    {
        public void LogError(string mensagem)
        {
            _logger.Error(mensagem);
        }

        public void LogInfo(string mensagem)
        {
            _logger.Information(mensagem);
        }
    }
}