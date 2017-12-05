using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearingMachineModels
{
    public class CurrentSimulationCase
    {
        public Bearing Bearing { get; set; }
        public int AccumulatedHours { get; set; }
        public int RandomDelay { get; set; }
        public int Delay { get; set; }
        public CurrentSimulationCase(Bearing Bearing, int AccumulatedHours, int RandomDelay, int Delay)
        {
            this.Bearing = Bearing;
            this.AccumulatedHours = AccumulatedHours;
            this.RandomDelay = RandomDelay;
            this.Delay = Delay;
        }
    }
}
