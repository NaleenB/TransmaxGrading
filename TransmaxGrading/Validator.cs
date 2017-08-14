using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransmaxGrading.DataModels;
using TransmaxGrading.Logger;

namespace TransmaxGrading
{
    public class Validator
    {
        ILogger logger;

        public Validator(ILogger logger)
        {
            this.logger = logger;
        }

        public bool ValidateAndParse(string fileName, out List<Scores> scores)
        {
            scores = new List<Scores>();

            //if file doesn't exist
            if (!File.Exists(fileName))
            {
                logger.LogMessage(LogSeverity.Warning, string.Format("File:{0} does not exist", fileName));
                return false;
            }

            string[] lines = System.IO.File.ReadAllLines(fileName);

            //if empty file
            if (lines.Length == 0)
            {
                logger.LogMessage(LogSeverity.Warning, string.Format("File:{0} is empty", fileName));
                return false;
            }

            int lineNum = 0;
            foreach (string line in lines)
            {
                lineNum++;

                string[] items = line.Split(',');

                //if line does not have a name and a score
                if (items.Length < 2)
                {
                    logger.LogMessage(LogSeverity.Warning, string.Format("File:{0}, line:{1} has missing name or score", fileName, lineNum));
                    return false;
                }

                items = items.Select(x => x.Trim()).ToArray();

                //if line has empty elements
                if (items.Any(x => string.IsNullOrEmpty(x)))
                {
                    logger.LogMessage(LogSeverity.Warning, string.Format("File:{0}, line:{1} has missing name or score", fileName, lineNum));
                    return false;
                }

                double score;
                if (!Double.TryParse(items.Last(), out score))
                {
                    //if last element of line is not score
                    logger.LogMessage(LogSeverity.Warning, string.Format("File:{0}, line:{1} has missing score as last element", fileName, lineNum));
                    return false;
                }

                Scores scoreObj = new Scores();

                scoreObj.Score = score;

                for (int i = 0; i < items.Length - 1; i++)
                {
                    scoreObj.Names.Add(items[i]);
                }

                scores.Add(scoreObj);
            }

            return true;
        }
    }
}
