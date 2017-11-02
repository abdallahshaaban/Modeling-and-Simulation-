using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewspaperSellerModels;
using NewspaperSellerSimulation;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    class CalculatePerformance
    {
        
        public CalculatePerformance()
        {   
        }

        public static double totalSalesRevenue(List<SimulationCase> simulation_table)
        {
            double ret = 0;
            for(int i = 0; i < simulation_table.Count; i++)
            {
                ret += simulation_table[i].SalesProfit;
            }
            return ret;
        }

        public static double totalCostOfNewspapers(List<SimulationCase> simulation_table)
        {
            double ret = 0;
            for (int i = 0; i < simulation_table.Count; i++)
            {
                ret += simulation_table[i].DailyCost;
            }
            return ret;
        }

        public static double totalLostProfit(List<SimulationCase> simulation_table)
        {
            double ret = 0;
            for (int i = 0; i < simulation_table.Count; i++)
            {
                ret += simulation_table[i].LostProfit;
            }
            return ret;
        }

        public static double totalSalvage(List<SimulationCase> simulation_table)
        {
            double ret = 0;
            for (int i = 0; i < simulation_table.Count; i++)
            {
                ret += simulation_table[i].ScrapProfit;
            }
            return ret;
        }

        public static double netProfit(List<SimulationCase> simulation_table)
        {
            double ret = 0;
            for (int i = 0; i < simulation_table.Count; i++)
            {
                ret += simulation_table[i].DailyNetProfit;
            }
            return ret;
        }

        public static int excessDemand(List<SimulationCase> simulation_table, int numOfNewspapers)
        {
            int ret = 0;
            for (int i = 0; i < simulation_table.Count; i++)
            {
                if(simulation_table[i].LostProfit > 0)
                {
                    ret = ret + 1;
                }
            }
            return ret;
        }

        public static int unsoldPapers(List<SimulationCase> simulation_table, int numOfNewspapers)
        {
            int ret = 0;
            for (int i = 0; i < simulation_table.Count; i++)
            {
                if (simulation_table[i].ScrapProfit > 0)
                {
                    ret = ret + 1;
                }
            }
            return ret;
        }
        

    }
}
