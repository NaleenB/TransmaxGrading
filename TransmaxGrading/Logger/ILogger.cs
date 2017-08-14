namespace TransmaxGrading.Logger
{
    public interface ILogger
    {
        void LogMessage(LogSeverity severity, string message);
    }
}
