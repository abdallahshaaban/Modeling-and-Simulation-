using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearingMachineModels
{
    public class Bearing
    {
        public int Index { get; set; }
        public int RandomHours { get; set; }
        public int Hours { get; set; }
        public Bearing(int Index , int RandomHours , int Hours)
        {
            this.Index = Index;
            this.RandomHours = RandomHours;
            this.Hours = Hours;
        }
    }
}
