using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearingMachineModels
{
    public class ProposedSimulationCase
    {
        public ProposedSimulationCase(List<Bearing> Bearings , int FirstFailure , int AccumulatedHours , int RandomDelay , int Delay)
        {
            Bearings = new List<Bearing>();
            this.Bearings = Bearings;
            this.FirstFailure = FirstFailure;
            this.AccumulatedHours = AccumulatedHours;
            this.RandomDelay = RandomDelay;
            this.Delay = Delay;
        }
        public List<Bearing> Bearings { get; set; }
        public int FirstFailure { get; set; }
        public int AccumulatedHours { get; set; }
        public int RandomDelay { get; set; }
        public int Delay { get; set; }
    }
}
