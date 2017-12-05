using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 
namespace BearingMachineModels
{
    class Helper
    {
        List<KeyValuePair<int, int>> StartAndEnd = new List<KeyValuePair<int, int>>();
        public void LoadData(string TestCasePath , SimulationSystem SimSystem)
        {
            string[] lines = System.IO.File.ReadAllLines(TestCasePath);
            for (int i = 0; i < lines.Count(); i++)
            {
                if (lines[i] == "DowntimeCost")
                {
                    i++;
                    SimSystem.DowntimeCost = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "RepairPersonCost")
                {
                    i++;
                    SimSystem.RepairPersonCost = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "BearingCost")
                {
                    i++;
                    SimSystem.BearingCost = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "NumberOfHours")
                {
                    i++;
                    SimSystem.NumberOfHours = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "NumberOfBearings")
                {
                    i++;
                    SimSystem.NumberOfBearings = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "RepairTimeForOneBearing")
                {
                    i++;
                    SimSystem.RepairTimeForOneBearing = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "RepairTimeForAllBearings")
                {
                    i++;
                    SimSystem.RepairTimeForAllBearings = Int32.Parse(lines[i]);
                }
                else if (lines[i] == "DelayTimeDistribution")
                {
                    i++;
                    while (lines[i] != "")
                    {
                        string[] dist = lines[i].Split(',');
                        SimSystem.DelayTimeDistribution.Add(new TimeDistribution(Int32.Parse(dist[0]), decimal.Parse(dist[1])));
                        i++;
                    }
                }
                else if (lines[i] == "BearingLifeDistribution")
                {
                    i++;
                    while (i < lines.Count())
                    {
                        string[] dist = lines[i].Split(',');
                        SimSystem.BearingLifeDistribution.Add(new TimeDistribution(Int32.Parse(dist[0]), decimal.Parse(dist[1])));
                        i++;
                    }
                    break;
                }
            }
        }
        public bool GetFullDistribution(List<TimeDistribution> l)
        {
            decimal cum = 0;
            int MaxPase = 10;
            for (int i = 0; i < l.Count(); i++)
            {
                cum += l[i].Probability;
                l[i].CummProbability = cum;
                decimal tmp = l[i].CummProbability * MaxPase;
                while (tmp % 1 != 0)
                {
                    tmp *= 10;
                    MaxPase *= 10;
                }
            }
            if (cum == 1)
            {
                int PrevEnd = 0;
                for (int i = 0; i < l.Count(); i++)
                {
                    l[i].MinRange = PrevEnd + 1;
                    decimal tmp = l[i].CummProbability * MaxPase;
                    PrevEnd = (int)tmp;
                    l[i].MaxRange = PrevEnd;
                }
            }
            else
            {
                MessageBox.Show("cumulative probability is NOT equal 1" + " " + cum.ToString());
                return false;
            }
            return true;
        }
        public int GetTime(int Random, List<TimeDistribution> l)
        {
            int i;
            for (i = 0; i < l.Count(); i++)
            {
                if (l[i].MaxRange >= Random) break;
            }
            return l[i].Time;
        }
        public void GetCurrentMethodSimulation(SimulationSystem SimSystem , SimulationOutput SimOutput)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index");
            dt.Columns.Add("Life RD");
            dt.Columns.Add("Life");
            dt.Columns.Add("Accumulated Life");
            dt.Columns.Add("Delay RD");
            dt.Columns.Add("Delay");
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            int NumberOfChangedBearings = 0;
            int TotalDelayTime = 0;
            int start = 0;
            for (int i = 0; i < SimSystem.NumberOfBearings; i++)
            {
                int index = 1;
                int AccumulatedHours = 0;
                int TotalDelay = 0;
                while (AccumulatedHours < SimSystem.NumberOfHours)
                {
                    int RndLifeTime = rnd1.Next(1, SimSystem.BearingLifeDistribution[SimSystem.BearingLifeDistribution.Count - 1].MaxRange);
                    int RndDelayTime = rnd2.Next(1, SimSystem.DelayTimeDistribution[SimSystem.DelayTimeDistribution.Count - 1].MaxRange);
                    int LifeTime = GetTime(RndLifeTime, SimSystem.BearingLifeDistribution);
                    int DelayTime = GetTime(RndDelayTime, SimSystem.DelayTimeDistribution);
                    AccumulatedHours += LifeTime;
                    TotalDelay += DelayTime;
                    SimSystem.CurrentSimulationCases.Add(new CurrentSimulationCase(new Bearing(index , RndLifeTime , LifeTime) , AccumulatedHours , RndDelayTime , DelayTime));
                    dt.Rows.Add(index.ToString() , RndLifeTime.ToString()  , LifeTime.ToString() , AccumulatedHours.ToString() , RndDelayTime.ToString() , DelayTime.ToString());
                    index++;
                    NumberOfChangedBearings++;
                }
                dt.Rows.Add('-' , '-' , '-' , '-' , '-' , TotalDelay.ToString());
                TotalDelayTime += TotalDelay;
                StartAndEnd.Add(new KeyValuePair<int, int>(start,(index-2) + start));
                start = (index - 2) + start + 1;
            }
            SimOutput.CurrentSimulation = dt;
            SimOutput.CurrentSimulationPerformance = GetCurrentMethodPerformance(SimSystem, NumberOfChangedBearings , TotalDelayTime);
        }
        DataTable GetCurrentMethodPerformance(SimulationSystem SimSystem , int NumberOfChangedBearings , int TotalDelayTime)
        {
            DataTable pdt = new DataTable();
            pdt.Columns.Add("Cost Type");
            pdt.Columns.Add("Value");
            SimSystem.CurrentPerformanceMeasures.BearingCost = NumberOfChangedBearings * SimSystem.BearingCost;
            pdt.Rows.Add("Cost of bearings", SimSystem.CurrentPerformanceMeasures.BearingCost.ToString() + '$');
            SimSystem.CurrentPerformanceMeasures.DelayCost = TotalDelayTime * SimSystem.DowntimeCost;
            pdt.Rows.Add("Cost of delay time", SimSystem.CurrentPerformanceMeasures.DelayCost.ToString() + '$');
            SimSystem.CurrentPerformanceMeasures.DowntimeCost = NumberOfChangedBearings * SimSystem.RepairTimeForOneBearing * SimSystem.DowntimeCost;
            pdt.Rows.Add("Cost of downtime during repair", SimSystem.CurrentPerformanceMeasures.DowntimeCost.ToString() + '$');
            SimSystem.CurrentPerformanceMeasures.RepairPersonCost = ((decimal)(NumberOfChangedBearings * SimSystem.RepairTimeForOneBearing) / 60) * SimSystem.RepairPersonCost;
            pdt.Rows.Add("Cost of repairpersons", SimSystem.CurrentPerformanceMeasures.RepairPersonCost.ToString() + '$');
            SimSystem.CurrentPerformanceMeasures.TotalCost = SimSystem.CurrentPerformanceMeasures.BearingCost + SimSystem.CurrentPerformanceMeasures.DelayCost + SimSystem.CurrentPerformanceMeasures.DowntimeCost + SimSystem.CurrentPerformanceMeasures.RepairPersonCost;
            pdt.Rows.Add("Total cost", SimSystem.CurrentPerformanceMeasures.TotalCost.ToString() + '$');
            return pdt;
        }
        public void GetProposedMethodSimulation(SimulationSystem SimSystem, SimulationOutput SimOutput)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Index");
            for(int i=0; i<SimSystem.NumberOfBearings; i++)
            {
                dt.Columns.Add("Bearing " + (i+1).ToString() + " Life");
            }
            dt.Columns.Add("First Failure");
            dt.Columns.Add("Accumulated Life");
            dt.Columns.Add("Delay RD");
            dt.Columns.Add("Delay");
            Random rnd1 = new Random();
            Random rnd2 = new Random();
            int NumberOfChangedBearings = 0;
            int TotalDelayTime = 0;
            int AccumulatedHours = 0;
            int index = 1;
            while (AccumulatedHours < SimSystem.NumberOfHours)
            {
                List<Bearing> Bearings = new List<Bearing>();
                NumberOfChangedBearings++;
                int MnLife = 99999999;
                for (int i = 0; i < SimSystem.NumberOfBearings; i++)
                {
                    if((StartAndEnd[i].Key + (index - 1)) <= StartAndEnd[i].Value)
                    {
                        Bearings.Add(SimSystem.CurrentSimulationCases[StartAndEnd[i].Key + (index - 1)].Bearing);
                        if (SimSystem.CurrentSimulationCases[StartAndEnd[i].Key + (index - 1)].Bearing.Hours < MnLife)
                        {
                            MnLife = SimSystem.CurrentSimulationCases[StartAndEnd[i].Key + (index - 1)].Bearing.Hours;
                        }
                    }
                    else
                    {
                        int RndLifeTime = rnd1.Next(1, SimSystem.BearingLifeDistribution[SimSystem.BearingLifeDistribution.Count - 1].MaxRange);
                        int LifeTime = GetTime(RndLifeTime, SimSystem.BearingLifeDistribution);
                        Bearings.Add(new Bearing(index, RndLifeTime, LifeTime));
                        if(LifeTime < MnLife)
                        {
                            MnLife = LifeTime;
                        }
                    }
                }
                AccumulatedHours += MnLife;
                int RndDelayTime = rnd2.Next(1, SimSystem.DelayTimeDistribution[SimSystem.DelayTimeDistribution.Count - 1].MaxRange);
                int DelayTime = GetTime(RndDelayTime, SimSystem.DelayTimeDistribution);
                TotalDelayTime += DelayTime;
                SimSystem.ProposedSimulationCases.Add(new ProposedSimulationCase(Bearings , MnLife , AccumulatedHours , RndDelayTime , DelayTime));
                DataRow dr = dt.NewRow();
                dr["Index"] = index.ToString();
                for (int i = 0; i < Bearings.Count() ; i++)
                {
                    string name = "Bearing " + (i + 1).ToString() + " Life";
                    dr[name] = Bearings[i].Hours.ToString();
                }
                dr["First Failure"] = MnLife.ToString();
                dr["Accumulated Life"] = AccumulatedHours.ToString();
                dr["Delay RD"] = RndDelayTime.ToString();
                dr["Delay"] = DelayTime.ToString();
                dt.Rows.Add(dr);
                index++;
            }
            DataRow rw = dt.NewRow();
            rw["Index"] = "-";
            for (int i = 0; i < SimSystem.NumberOfBearings; i++)
            {
                string name = "Bearing " + (i + 1).ToString() + " Life";
                rw[name] = "-";
            }
            rw["First Failure"] = "-";
            rw["Accumulated Life"] = "-";
            rw["Delay RD"] = "-";
            rw["Delay"] = TotalDelayTime.ToString();
            dt.Rows.Add(rw);
            SimOutput.ProposedSimulation = dt;
            SimOutput.ProposedSimulationPerformance = GetProposedMethodPerformance(SimSystem, NumberOfChangedBearings, TotalDelayTime);
        }
        DataTable GetProposedMethodPerformance(SimulationSystem SimSystem, int NumberOfChangedBearings, int TotalDelayTime)
        {
            DataTable pdt = new DataTable();
            pdt.Columns.Add("Cost Type");
            pdt.Columns.Add("Value");
            SimSystem.ProposedPerformanceMeasures.BearingCost = (NumberOfChangedBearings*3) * SimSystem.BearingCost;
            pdt.Rows.Add("Cost of bearings", SimSystem.ProposedPerformanceMeasures.BearingCost.ToString() + '$');
            SimSystem.ProposedPerformanceMeasures.DelayCost = TotalDelayTime * SimSystem.DowntimeCost;
            pdt.Rows.Add("Cost of delay time", SimSystem.ProposedPerformanceMeasures.DelayCost.ToString() + '$');
            SimSystem.ProposedPerformanceMeasures.DowntimeCost = NumberOfChangedBearings * SimSystem.RepairTimeForAllBearings * SimSystem.DowntimeCost;
            pdt.Rows.Add("Cost of downtime during repair", SimSystem.ProposedPerformanceMeasures.DowntimeCost.ToString() + '$');
            SimSystem.ProposedPerformanceMeasures.RepairPersonCost = ((decimal)(NumberOfChangedBearings * SimSystem.RepairTimeForAllBearings) / 60) * SimSystem.RepairPersonCost;
            pdt.Rows.Add("Cost of repairpersons", SimSystem.ProposedPerformanceMeasures.RepairPersonCost.ToString() + '$');
            SimSystem.ProposedPerformanceMeasures.TotalCost = SimSystem.ProposedPerformanceMeasures.BearingCost + SimSystem.ProposedPerformanceMeasures.DelayCost + SimSystem.ProposedPerformanceMeasures.DowntimeCost + SimSystem.ProposedPerformanceMeasures.RepairPersonCost;
            pdt.Rows.Add("Total cost", SimSystem.ProposedPerformanceMeasures.TotalCost.ToString() + '$');
            return pdt;
        }
    }
}
