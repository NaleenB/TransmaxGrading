using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        /// <summary>
        /// Validates file content
        /// Parses the file content in to a list of objects
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="scores"></param>
        /// <returns></returns>
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
                if (items.Length != 3)
                {
                    logger.LogMessage(LogSeverity.Warning, string.Format("File:{0}, line:{1} has invalid format. Please use last name, first name followed by score", fileName, lineNum));
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

                scoreObj.LastName = items[0];

                scoreObj.FirstName = items[1];

                scores.Add(scoreObj);
            }

            return true;
        }
    }
}
