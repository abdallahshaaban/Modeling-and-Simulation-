﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearingMachineModels
{
    public class TimeDistribution
    {
        public int Time { get; set; }
        public decimal Probability { get; set; }
        public decimal CummProbability { get; set; }
        public int MinRange { get; set; }
        public int MaxRange { get; set; }
        //public TimeDistribution(int time , decimal probability)
        //{
        //    this.Time = time;
        //    this.Probability = probability;
        //}
    }
}
