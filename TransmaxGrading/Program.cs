using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransmaxGrading.DataModels;
using TransmaxGrading.Logger;

namespace TransmaxGrading
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initializing type of logger
            ILogger logger = new FileLogger();

            //Initial entry validation
            if (args.Length != 1)
            {
                logger.LogMessage(LogSeverity.Warning, "only one parameter in the format of 'filename.txt' supported");
                return;
            }

            //File validation and passing
            List<Scores> scores;
            if (!InputValidator.Validate(args[0], out scores))
            {
                return;
            }

            //Processing


            //Writing to output file
        }
    }
}
