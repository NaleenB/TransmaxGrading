using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransmaxGrading.DataModels
{
    public class Scores
    {
        public Scores()
        {
            Names = new List<string>();
        }

        public double Score { get; set; }

        public List<string> Names { get; set; }
    }
}
