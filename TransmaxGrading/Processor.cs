using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using TransmaxGrading.DataModels;
using TransmaxGrading.Logger;

namespace TransmaxGrading
{
    public class Processor
    {
        ILogger logger;

        public Processor(ILogger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// order list by 
        /// 1. score descending
        /// 2. last name
        /// 3. first name
        /// </summary>
        /// <param name="scores"></param>
        /// <returns></returns>
        public List<Scores> OrderList(List<Scores> scores)
        {
            return scores
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.LastName)
                .ThenBy(x => x.FirstName)
                .ToList();
        }

        /// <summary>
        /// save new ordered list to file
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="fileContent"></param>
        public void SaveNewFile(string fileName, List<Scores> fileContent)
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path += "\\" + fileName;

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                using (StreamWriter w = File.AppendText(path))
                {
                    foreach (Scores score in fileContent)
                    {
                        w.WriteLine("{0}, {1}, {2}", score.LastName, score.FirstName, score.Score);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogMessage(LogSeverity.Error, ex.Message);
            }

            logger.LogMessage(LogSeverity.Info, string.Format("File:{0} created", fileName));
        }
    }
}
