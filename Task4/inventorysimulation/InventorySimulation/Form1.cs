using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InventoryTesting;
using InventoryModels;
namespace InventorySimulation
{
    public partial class Form1 : Form
    {
        public List<Distribution> LeadTimeDis;
        public List<Distribution> demandDis;
        public Form1()
        {
            InitializeComponent();
         }

        private void Form1_Load(object sender, EventArgs e)
        {
            LeadTimeDis = new List<Distribution>();
            demandDis = new List<Distribution>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fileName;
            fileName = txtFileName.Text;
            string[] lines = System.IO.File.ReadAllLines(fileName);

            // Display the file contents by using a foreach loop.
            //System.Console.WriteLine("Contents of WriteLines2.txt = ");
            for (int i = 0; i < lines.Count(); ++i)
            {
                // Use a tab to indent each line of the file.
                if (lines[i] == "OrderUpTo") theOrderLevel.Text = lines[i + 1];
                if (lines[i] == "ReviewPeriod") TheReviewPeriod.Text = lines[i + 1];
                if (lines[i] == "StartInventoryQuantity") BeginningInventoryQuantity.Text = lines[i + 1];
                if (lines[i] == "StartLeadDays") FirstOrderarrivesAfter.Text = lines[i + 1];
                if (lines[i] == "StartOrderQuantity") FirstOrderQuantity.Text = lines[i + 1];
                if (lines[i] == "NumberOfDays") NumberOfDays.Text = lines[i + 1];

                if (lines[i] == "LeadDaysDistribution")
                {

                    int a = 0;
                    for (int j = i + 1; j < lines.Count(); ++j)
                    {
                        string[] values = lines[j].Split(',');
                        List<float> value = new List<float>(values.Length);
                        dataGridView2.Rows.Add();
                        for (int k = 0; k < values.Length; k++)
                        {
                            value.Add(new float());
                            value[k] = float.Parse(values[k]);
                            dataGridView2.Rows[a].Cells[k].Value = value[k];
                        }

                        a++;
                    }
                }
                if (lines[i] == "DemandDistribution")
                {
                    int q = 0;
                    for (int j = i + 1; j < lines.Count(); ++j)
                    {
                        if (lines[j] == "") break;
                        string[] values = lines[j].Split(',');
                        List<float> value = new List<float>(values.Length);
                        dataGridView1.Rows.Add();
                        for (int k = 0; k < values.Length; k++)
                        {
                            value.Add(new float());
                            value[k] = float.Parse(values[k]);
                            dataGridView1.Rows[q].Cells[k].Value = value[k];
                        }

                        q++;
                        //dataGridView1.DataSource = value;


                    }
                }
            }



        }
        private void Read_Demand_Distribution()
        {
            int m = dataGridView1.Rows.Count;
            decimal cumProbability = 0;
        

            for (int i = 0; i < m-1 ; ++i)
            {
                cumProbability += Math.Round(decimal.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()), 5);

              

            }
            if (cumProbability == 1 )
            {
              
                for (int i = 0; i < m-1 ; ++i)
                {

                    demandDis.Add(new Distribution());
                    demandDis[i].Value = Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    demandDis[i].Probability= Math.Round(decimal.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()), 5);




                }



            }
            else { MessageBox.Show("cumulative probability is NOT equal 1"); }
        }

        private void Read_Lead_time_Distribution()
        {
            int m = dataGridView2.Rows.Count;
            decimal cumProbability = 0;
           

            for (int i = 0; i < m - 1; ++i)
            {
                cumProbability += Math.Round(decimal.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 5);



            }
            if (cumProbability == 1)
            {
                // MessageBox.Show(m.ToString());
                for (int i = 0; i < m - 1; ++i)
                {

                    LeadTimeDis.Add(new Distribution());
                    LeadTimeDis[i].Value = Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                    LeadTimeDis[i].Probability = Math.Round(decimal.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 5);




                }



            }
            else { MessageBox.Show("cumulative probability is NOT equal 1"); }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            InventoryModels.SimulationSystem simsys = new InventoryModels.SimulationSystem();
            LeadTimeDis = new List<Distribution>();
            demandDis = new List<Distribution>();

            Read_Demand_Distribution();
            Read_Lead_time_Distribution();
            Helper.Calc_Ranges(LeadTimeDis);
            Helper.Calc_Ranges(demandDis);
            simsys.DemandDistribution = demandDis;
            simsys.LeadDaysDistribution = LeadTimeDis;
           // MessageBox.Show(LeadTimeDis.Count.ToString());

            simsys.NumberOfDays = Int32.Parse(NumberOfDays.Text.ToString());
            simsys.OrderUpTo = Int32.Parse(theOrderLevel.Text.ToString());
            simsys.ReviewPeriod = Int32.Parse(TheReviewPeriod.Text.ToString());
            simsys.StartOrderQuantity = Int32.Parse(FirstOrderQuantity.Text.ToString());
            simsys.StartLeadDays = Int32.Parse(FirstOrderarrivesAfter.Text.ToString());
            simsys.StartInventoryQuantity = Int32.Parse(BeginningInventoryQuantity.Text.ToString());


            List<SimulationCase> simulation = Helper.simulate(simsys);
            simsys.SimulationCases = simulation;
            simsys.PerformanceMeasures.EndingInventoryAverage = Helper.endingInvenoryAverage(simulation);
            simsys.PerformanceMeasures.ShortageQuantityAverage = Helper.shortageQuantityAverage(simulation);
            dataGridView3.DataSource = simulation;
            

            string testing = TestingManager.Test(simsys, Constants.FileNames.TestCase1);
            MessageBox.Show(testing);
        }
    }
}
