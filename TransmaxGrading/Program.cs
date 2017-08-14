using System.Collections.Generic;
using System.IO;
using TransmaxGrading.DataModels;
using TransmaxGrading.Logger;

namespace TransmaxGrading
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initializing type of logger to be used in the app
            //can use reflections to initialize dynamic assembly
            ILogger logger = new FileLogger();

            //Initial entry validation
            if (args.Length != 1)
            {
                logger.LogMessage(LogSeverity.Warning, "only one parameter in the format of 'filename.txt' supported");
                return;
            }

            Validator validator = new Validator(logger);

            //File validation and passing
            List<Scores> scores;
            if (!validator.ValidateAndParse(args[0], out scores))
            {
                return;
            }

            //Processing
            Processor processor = new Processor(logger);
            scores = processor.OrderList(scores);

            //Saving new file
            string newFileName = Path.GetFileNameWithoutExtension(args[0]);
            newFileName += "-graded.txt";
            processor.SaveNewFile(newFileName, scores);
        }
    }
}
