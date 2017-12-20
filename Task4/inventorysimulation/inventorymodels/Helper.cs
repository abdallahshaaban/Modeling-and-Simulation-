using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModels
{
    public static class Helper
    {

        public static void Calc_Ranges(List<Distribution> distrib)
        {
            decimal CurEnd = 0;
            decimal CurCum = 0;
            for (int i = 0; i < distrib.Count; i++)
            {
                distrib[i].CummProbability = CurCum + distrib[i].Probability;
                CurCum = distrib[i].CummProbability;
                distrib[i].MinRange = Int32.Parse((CurEnd + 1).ToString());
                distrib[i].MaxRange = Int32.Parse((CurEnd + Convert.ToInt32(distrib[i].Probability * 100)).ToString());
               
                CurEnd = distrib[i].MaxRange;
            }
        }

        public static int demandAndLead(int number, List<Distribution> distribution) {
            int n=0;
            for (int i = 0; i < distribution.Count; ++i)
            {
               
                if (number >= distribution[i].MinRange && number <= distribution[i].MaxRange)
                    return distribution[i].Value;
            }
            return n;
        }
        public static decimal endingInvenoryAverage(List<SimulationCase> simCases) {
            decimal av = 0;
            for (int i = 0; i < simCases.Count; ++i) {
                av += simCases[i].EndingInventory;
            }
            av /= simCases.Count;

            return av;
        }
        public static decimal shortageQuantityAverage(List<SimulationCase> simCases) {
            decimal av = 0;
            for (int i = 0; i < simCases.Count; ++i)
            {
                av += simCases[i].ShortageQuantity;
            }
            av /= simCases.Count;

            return av;
        }
        
        public static List<SimulationCase> simulate(SimulationSystem simSys) {
            List<SimulationCase> simCase = new List<SimulationCase>();
            Random random = new Random();
            int beginning = simSys.StartInventoryQuantity;
            int leadingday = simSys.StartLeadDays;
            int order = simSys.StartOrderQuantity;
            int shortage = 0;
            List<int> T = new List<int> { 26,68,33,39,86,18,64,79,55,74,21,43,49,90,35,8,98,61,85,81,53,15,94,19,44};
            List<int> R = new List<int> { 0,0,0,0,8,0,0,0,0,7,0,0,0,0,2,0,0,0,0,3,0,0,0,0,1 };
            int index = 1;
            for (int i = 0; i < simSys.NumberOfDays; ++i) {



                simCase.Add(new SimulationCase());
                simCase[i].Day = i + 1;
                simCase[i].Cycle = (i / simSys.ReviewPeriod) + 1;
                simCase[i].DayWithinCycle = index;
                simCase[i].BeginningInventory = simSys.StartOrderQuantity;

                simCase[i].BeginningInventory = beginning;
                 int randomNumber = random.Next(1, 100);
               // int randomNumber = T[i];
                simCase[i].RandomDemand = randomNumber;
                simCase[i].Demand = demandAndLead(randomNumber, simSys.DemandDistribution);


                if (simCase[i].BeginningInventory - simCase[i].Demand - shortage > 0)
                { simCase[i].EndingInventory = simCase[i].BeginningInventory - simCase[i].Demand - shortage;shortage = 0; }
                    else {
                        simCase[i].EndingInventory = 0;
                    shortage += -1 * (simCase[i].BeginningInventory - simCase[i].Demand);
                        simCase[i].ShortageQuantity =shortage;

                    }
                if (leadingday == 1) { beginning = simCase[i].EndingInventory + order   ;
                        }
                else { beginning = simCase[i].EndingInventory; }
                 
                --leadingday;
                if (index == simSys.ReviewPeriod)
                {
                     randomNumber = random.Next(1, 100);
                   // randomNumber = R[i];
                    simCase[i].RandomLeadDays = randomNumber;
                    simCase[i].LeadDays = demandAndLead(randomNumber, simSys.LeadDaysDistribution);
                    leadingday = simCase[i].LeadDays;
                    simCase[i].OrderQuantity = (simSys.OrderUpTo) - (simCase[i].EndingInventory) + (shortage);
                    order = simCase[i].OrderQuantity;

                }
                
                ++index;
               
                if (index > simSys.ReviewPeriod) index = 1;

                

    }



            return simCase;

        }



       
    }



}

