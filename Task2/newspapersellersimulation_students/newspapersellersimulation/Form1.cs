using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerTesting;
using NewspaperSellerModels;

namespace NewspaperSellerSimulation
{
    public partial class Form1 : Form
    {
        public List<DayTypeDistribution> dayTypeDis;
        public List<DemandDistribution> demandDis;
        public Form1()
        {
            InitializeComponent();            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dayTypeDis = new List<DayTypeDistribution>();
            demandDis = new List<DemandDistribution>();
        }
        private void Read_Demand_Distribution()
        {
            int m = dataGridView1.Rows.Count;
            double cumlativeGoodDay = 0;
            double cumlativeFairDay = 0;
            double cumlativePoorDay = 0;

            for (int i = 0; i < m - 1; ++i) {
                cumlativeGoodDay += Math.Round(float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()), 5);
                cumlativeFairDay += Math.Round(float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()), 5);
                cumlativePoorDay += Math.Round(float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()), 5);


            }
            if (cumlativeFairDay == 1 && cumlativeGoodDay == 1 && cumlativePoorDay == 1)
            {
                
                for (int i = 0; i < m - 1; ++i)
                {

                    demandDis.Add(new DemandDistribution());
                    demandDis[i].Demand = Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    double test = demandDis[i].Demand % 10;

                    if (test != 0.0)
                    {
                        string error = "demand # : " + i + "is not multiple of 10";
                        MessageBox.Show(error);
                        demandDis.Clear();
                    }
                    else
                    {

                        demandDis[i].DayTypeDistributions.Add(new DayTypeDistribution());
                        demandDis[i].DayTypeDistributions[0].Probability = Math.Round(float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()), 5);
                        demandDis[i].DayTypeDistributions[0].DayType = Enums.DayType.Good;
                        demandDis[i].DayTypeDistributions.Add(new DayTypeDistribution());

                        demandDis[i].DayTypeDistributions[1].Probability = Math.Round(float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()), 5);
                        demandDis[i].DayTypeDistributions[1].DayType = Enums.DayType.Fair;

                        demandDis[i].DayTypeDistributions.Add(new DayTypeDistribution());
                        demandDis[i].DayTypeDistributions[2].Probability = Math.Round(float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString()), 5);
                        demandDis[i].DayTypeDistributions[2].DayType = Enums.DayType.Poor;

                    }

                }



            }
            else { MessageBox.Show("cumulative probability is NOT equal 1"); }
        }


        private void Read_Day_Type_Distribution()
        {
            int m = dataGridView2.Rows.Count;
            if (m != 2)
            {
                MessageBox.Show("Day Type Distribution must be one Row");
            }
            else {
                double test = 0;
                test += Math.Round(float.Parse(dataGridView2.Rows[0].Cells[0].Value.ToString()), 5);
                test += Math.Round(float.Parse(dataGridView2.Rows[0].Cells[1].Value.ToString()), 5);
                test += Math.Round(float.Parse(dataGridView2.Rows[0].Cells[2].Value.ToString()), 5);


                if (test == 1)
                {
                    dayTypeDis.Add(new DayTypeDistribution());
                    dayTypeDis[0].Probability = Math.Round(float.Parse(dataGridView2.Rows[0].Cells[0].Value.ToString()), 5);
                    dayTypeDis[0].DayType = Enums.DayType.Good;
                    dayTypeDis.Add(new DayTypeDistribution());
                    dayTypeDis[1].Probability = Math.Round(float.Parse(dataGridView2.Rows[0].Cells[1].Value.ToString()), 5);
                    dayTypeDis[1].DayType = Enums.DayType.Fair;
                    dayTypeDis.Add(new DayTypeDistribution());
                    dayTypeDis[2].Probability = Math.Round(float.Parse(dataGridView2.Rows[0].Cells[2].Value.ToString()), 5);
                    dayTypeDis[2].DayType = Enums.DayType.Poor;
                }
                else { MessageBox.Show("cumulative probability is NOT equal 1"); }
            }
        }
        private void btnReadData_Click(object sender, EventArgs e)
        {
            NewspaperSellerModels.System sys = new NewspaperSellerModels.System();

            dayTypeDis = new List<DayTypeDistribution>();
            demandDis = new List<DemandDistribution>();
            //string test = TestingManager.Test(sys, Constants.FileNames.TestCase1);
            //MessageBox.Show(test);

            Read_Demand_Distribution();
            Read_Day_Type_Distribution();
            Helper.Calc_Ranges(dayTypeDis);
            Helper.Calc_Ranges_Of_Demand(demandDis);

            //test
            /*for (int i = 0; i < demandDis.Count; i++) {
                  MessageBox.Show(demandDis[i].DayTypeDistributions[0].MinRange.ToString());
                  MessageBox.Show(demandDis[i].DayTypeDistributions[0].MaxRange.ToString());
                  MessageBox.Show(demandDis[i].DayTypeDistributions[1].MinRange.ToString());
                  MessageBox.Show(demandDis[i].DayTypeDistributions[1].MaxRange.ToString());
                  MessageBox.Show(demandDis[i].DayTypeDistributions[2].MinRange.ToString());
                  MessageBox.Show(demandDis[i].DayTypeDistributions[2].MaxRange.ToString());
              }

            for (int i = 0; i < dayTypeDis.Count; i++)
            {
                MessageBox.Show(dayTypeDis[i].CummProbability.ToString());
                MessageBox.Show(dayTypeDis[i].MinRange.ToString());
                MessageBox.Show(dayTypeDis[i].MaxRange.ToString());
                
            }
            */
            sys.NumOfNewspapers = Int32.Parse( txtNumOfNewpapers.Text.ToString());
            sys.NumOfRecords = Int32.Parse(txtNumOfDays.Text.ToString());
            sys.PurchasePrice= Math.Round(float.Parse(txtPurchasePrice.Text.ToString()),5);
            sys.ScrapPrice = Math.Round(float.Parse(txtScrapPrice.Text.ToString()),5);
            sys.SellingPrice = Math.Round(float.Parse(txtSellingPrice.Text.ToString()),5);
            sys.DayTypeDistributions = dayTypeDis;
            sys.DemandDistributions = demandDis;
            sys.UnitProfit = sys.PurchasePrice - sys.SellingPrice;
            List<SimulationCase> simulation = Helper.simulate(sys);
            dataGridView3.DataSource = simulation;
            sys.PerformanceMeasures.TotalSalesProfit = CalculatePerformance.totalSalesRevenue(simulation);
            sys.PerformanceMeasures.TotalCost = CalculatePerformance.totalCostOfNewspapers(simulation);
            sys.PerformanceMeasures.TotalLostProfit = CalculatePerformance.totalLostProfit(simulation);
            sys.PerformanceMeasures.TotalScrapProfit = CalculatePerformance.totalSalvage(simulation);
            sys.PerformanceMeasures.TotalNetProfit = CalculatePerformance.netProfit(simulation);
            sys.PerformanceMeasures.DaysWithMoreDemand = CalculatePerformance.excessDemand(simulation, sys.NumOfNewspapers);
            sys.PerformanceMeasures.DaysWithUnsoldPapers = CalculatePerformance.unsoldPapers(simulation, sys.NumOfNewspapers);
            //MessageBox.Show(a.ToString());
            //MessageBox.Show(b.ToString());
            //MessageBox.Show(c.ToString());
            //MessageBox.Show(d.ToString());
            //MessageBox.Show(E.ToString());
            //MessageBox.Show(f.ToString());
            //MessageBox.Show(g.ToString());
            
            

        }
    }
}
