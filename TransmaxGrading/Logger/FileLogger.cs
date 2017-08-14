using System;
using System.IO;
using System.Reflection;

namespace TransmaxGrading.Logger
{
    /// <summary>
    /// writing a simple file logger instead of using
    /// enterprise library or log4net
    /// </summary>
    public class FileLogger : ILogger
    {
        string logFileName = "AppLog.txt";

        public void LogMessage(LogSeverity severity, string message)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            try
            {
                using (StreamWriter w = File.AppendText(path + "\\" + logFileName))
                {
                    w.WriteLine("[{0}] [{1}] {2}", DateTime.Now.ToString(), severity, message);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
