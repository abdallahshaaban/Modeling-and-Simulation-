using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BearingMachineModels
{
    public class SimulationSystem
    {
        public SimulationSystem()
        {
            DelayTimeDistribution = new List<TimeDistribution>();
            BearingLifeDistribution = new List<TimeDistribution>();
            CurrentSimulationCases = new List<CurrentSimulationCase>();
            CurrentPerformanceMeasures = new PerformanceMeasures();
            ProposedSimulationCases = new List<ProposedSimulationCase>();
            ProposedPerformanceMeasures = new PerformanceMeasures();
        }
        public SimulationOutput StartSimulation(string TestCasePath)
        {
            SimulationOutput SimOutput = new SimulationOutput();
            Helper helper = new Helper();
            helper.LoadData(TestCasePath,this);
            if (!helper.GetFullDistribution(DelayTimeDistribution)) return SimOutput;
            if (!helper.GetFullDistribution(BearingLifeDistribution)) return SimOutput;
            helper.GetCurrentMethodSimulation(this, SimOutput);
            helper.GetProposedMethodSimulation(this,SimOutput);
            return SimOutput;
        }
        ///////////// INPUTS /////////////
        public int DowntimeCost { get; set; }
        public int RepairPersonCost { get; set; }
        public int BearingCost { get; set; }
        public int NumberOfHours { get; set; }
        public int NumberOfBearings { get; set; }
        public int RepairTimeForOneBearing { get; set; }
        public int RepairTimeForAllBearings { get; set; }
        public List<TimeDistribution> DelayTimeDistribution { get; set; }
        public List<TimeDistribution> BearingLifeDistribution { get; set; }

        ///////////// OUTPUTS /////////////
        public List<CurrentSimulationCase> CurrentSimulationCases { get; set; }
        public PerformanceMeasures CurrentPerformanceMeasures { get; set; }
        public List<ProposedSimulationCase> ProposedSimulationCases { get; set; }
        public PerformanceMeasures ProposedPerformanceMeasures { get; set; }        
    }
}
