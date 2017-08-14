using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransmaxGrading.Logger
{
    public interface ILogger
    {
        void LogMessage(LogSeverity severity, string message);
    }
}
