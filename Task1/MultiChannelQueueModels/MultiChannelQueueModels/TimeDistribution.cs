using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChannelQueueModels
{
    public class TimeDistribution
    {
        public int Time { get; set; }

        public double Probability { get; set; }

        public double CummProbability { get; set; }

        public double MinRange { get; set; }

        public double MaxRange { get; set; }
        public TimeDistribution(int Time , double Probability , double CummProbability , double MinRange , double MaxRange)
        {
            this.Time = Time;
            this.Probability = Probability;
            this.CummProbability = CummProbability;
            this.MinRange = MinRange;
            this.MaxRange = MaxRange;
        }
    }
}
