using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewspaperSellerModels
{
    public static class Helper
    {
        
        public static void Calc_Ranges(List<DayTypeDistribution> dayTypeDis)
        {
            double CurEnd = 0;
            double CurCum = 0;
            for (int i = 0; i < dayTypeDis.Count; i++)
            {
                dayTypeDis[i].CummProbability = CurCum + dayTypeDis[i].Probability;
                CurCum = dayTypeDis[i].CummProbability;
                dayTypeDis[i].MinRange = Int32.Parse((CurEnd + 1).ToString());
                dayTypeDis[i].MaxRange = Int32.Parse((CurEnd + dayTypeDis[i].Probability * 100).ToString());
                CurEnd = dayTypeDis[i].MaxRange;
            }
        }
        public static void Calc_Ranges_Of_Demand(List<DemandDistribution> demandDist) {

            double CurEndOfGoodDay = 0;
            double CurCumOfGoodDay = 0;
            double CurEndOfFairDay = 0;
            double CurCumOfFairDay = 0;
            double CurEndOfPoorDay = 0;
            double CurCumOfPoorDay = 0;
            for (int i = 0; i < demandDist.Count; i++)
            {
                demandDist[i].DayTypeDistributions[0].CummProbability = CurCumOfGoodDay + demandDist[i].DayTypeDistributions[0].Probability;
                CurCumOfGoodDay = demandDist[i].DayTypeDistributions[0].CummProbability;
                demandDist[i].DayTypeDistributions[0].MinRange = Int32.Parse((CurEndOfGoodDay + 1).ToString());
                demandDist[i].DayTypeDistributions[0].MaxRange = Int32.Parse((CurEndOfGoodDay+ demandDist[i].DayTypeDistributions[0].Probability*100).ToString());
                CurEndOfGoodDay = demandDist[i].DayTypeDistributions[0].MaxRange;
                demandDist[i].DayTypeDistributions[0].CummProbability = CurCumOfFairDay + demandDist[i].DayTypeDistributions[1].Probability;
                CurCumOfFairDay = demandDist[i].DayTypeDistributions[1].CummProbability;

                demandDist[i].DayTypeDistributions[1].MinRange = Int32.Parse((CurEndOfFairDay + 1).ToString());
                demandDist[i].DayTypeDistributions[1].MaxRange = Int32.Parse((CurEndOfFairDay + demandDist[i].DayTypeDistributions[1].Probability * 100).ToString());
                CurEndOfFairDay = demandDist[i].DayTypeDistributions[1].MaxRange;

                demandDist[i].DayTypeDistributions[0].CummProbability = CurCumOfPoorDay + demandDist[i].DayTypeDistributions[2].Probability;
                CurCumOfPoorDay = demandDist[i].DayTypeDistributions[2].CummProbability;
                demandDist[i].DayTypeDistributions[2].MinRange = Int32.Parse((CurEndOfPoorDay + 1).ToString());
                demandDist[i].DayTypeDistributions[2].MaxRange = Int32.Parse((CurEndOfPoorDay + demandDist[i].DayTypeDistributions[2].Probability * 100).ToString());
                CurEndOfPoorDay = demandDist[i].DayTypeDistributions[2].MaxRange;
            }


        }

        public static Enums.DayType news_dayType(List<DayTypeDistribution>dayType,int number) {
            
            for (int i = 0; i < 3; i++) {
                if (number >= dayType[i].MinRange && number <= dayType[i].MaxRange)
                    return (Enums.DayType)i;
            }
            return (Enums.DayType)0;
        }
        public static int demand(int number , Enums.DayType type,List<DemandDistribution> distribution) {
            int x=0;
            if (type == Enums.DayType.Good) {
                for (int i = 0; i < distribution.Count; ++i) {
                    if (number >= distribution[i].DayTypeDistributions[0].MinRange && number <= distribution[i].DayTypeDistributions[0].MaxRange)
                        return distribution[i].Demand;
                }
            }
            else if (type == Enums.DayType.Fair) {
                for (int i = 0; i < distribution.Count; ++i)
                {
                    if (number >= distribution[i].DayTypeDistributions[1].MinRange && number <= distribution[i].DayTypeDistributions[1].MaxRange)
                        return distribution[i].Demand;
                }
            }
            else if (type == Enums.DayType.Poor) {
                for (int i = 0; i < distribution.Count; ++i)
                {
                    if (number >= distribution[i].DayTypeDistributions[2].MinRange && number <= distribution[i].DayTypeDistributions[2].MaxRange)
                        return distribution[i].Demand;
                }
            }
            return x;
        }
        public static List<SimulationCase> simulate(NewspaperSellerModels.System sys) {
            List<SimulationCase> simulation = new List<SimulationCase>();
            List<int> R =new List<int> {94,77,49,45,43,32,49,100,16,24,31,14,41,61,85,8,15,97,52,78 };
            List<int> T =new List<int> {80,20,15,88,98,65,86,73,24,60,60,29,18,90,93,73,21,45,76,96 };
            Random random = new Random();
            for (int i = 0; i < sys.NumOfRecords; ++i) {
                simulation.Add(new SimulationCase());
                simulation[i].DayNo = i + 1;

                //int randomNumber = random.Next(0, 100);
                int randomNumber = R[i];
                // simulation[i].RandomNewsDayType = randomNumber;
                simulation[i].RandomNewsDayType = R[i];
                simulation[i].NewsDayType = news_dayType(sys.DayTypeDistributions,randomNumber);
                //andomNumber = random.Next(0, 100);
                randomNumber = T[i];
                //simulation[i].RandomDemand = randomNumber;
                simulation[i].RandomDemand = T[i];
                simulation[i].Demand = demand(randomNumber, simulation[i].NewsDayType,sys.DemandDistributions);
                //if(simulation[i].Demand<sys.NumOfNewspapers)
                    simulation[i].DailyCost = Math.Round(sys.NumOfNewspapers * sys.SellingPrice,2);
                if (simulation[i].Demand > sys.NumOfNewspapers)
                { simulation[i].LostProfit = Math.Round((simulation[i].Demand - sys.NumOfNewspapers) * sys.UnitProfit,2);
                    simulation[i].SalesProfit = sys.PurchasePrice * sys.NumOfNewspapers;
                }
                else simulation[i].LostProfit = 0;

                if (simulation[i].Demand < sys.NumOfNewspapers)
                { simulation[i].ScrapProfit = (sys.NumOfNewspapers - simulation[i].Demand) * sys.ScrapPrice;
                    simulation[i].SalesProfit = (simulation[i].Demand) * sys.PurchasePrice;
                }
       
                if (simulation[i].Demand == sys.NumOfNewspapers)
                    simulation[i].SalesProfit = sys.PurchasePrice * sys.NumOfNewspapers;
                simulation[i].DailyNetProfit= Math.Round(simulation[i].SalesProfit - (sys.SellingPrice * sys.NumOfNewspapers)- simulation[i].LostProfit + simulation[i].ScrapProfit,2);
            }


            return simulation;
        }


    }
 }

