using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BearingMachineTesting;
using BearingMachineModels;

namespace BearingMachineSimulation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TestCasePath = txttestcase.Text.ToString();
            SimulationSystem SimSystem = new SimulationSystem();
            Display(SimSystem.StartSimulation(TestCasePath) , SimSystem);
            string TestResult = TestingManager.Test(SimSystem, Constants.FileNames.TestCase1);
            MessageBox.Show(TestResult);
            //For Testing
            /*
            MessageBox.Show(SimSystem.DowntimeCost.ToString());
            MessageBox.Show(SimSystem.RepairPersonCost.ToString());
            MessageBox.Show(SimSystem.BearingCost.ToString());
            MessageBox.Show(SimSystem.NumberOfHours.ToString());
            MessageBox.Show(SimSystem.NumberOfBearings.ToString());
            MessageBox.Show(SimSystem.RepairTimeForOneBearing.ToString());
            MessageBox.Show(SimSystem.RepairTimeForAllBearings.ToString());
            for(int i=0; i<SimSystem.DelayTimeDistribution.Count(); i++) MessageBox.Show(SimSystem.DelayTimeDistribution[i].Time.ToString() + " " + SimSystem.DelayTimeDistribution[i].Probability.ToString() + " " + SimSystem.DelayTimeDistribution[i].CummProbability.ToString() + " " + SimSystem.DelayTimeDistribution[i].MinRange.ToString() + " " + SimSystem.DelayTimeDistribution[i].MaxRange.ToString());
            for (int i = 0; i < SimSystem.BearingLifeDistribution.Count(); i++) MessageBox.Show(SimSystem.BearingLifeDistribution[i].Time.ToString() + " " + SimSystem.BearingLifeDistribution[i].Probability.ToString() + " " + SimSystem.BearingLifeDistribution[i].CummProbability.ToString() + " " + SimSystem.BearingLifeDistribution[i].MinRange.ToString() + " " + SimSystem.BearingLifeDistribution[i].MaxRange.ToString());
            */
        }
        void Display(BearingMachineModels.SimulationOutput SimOutput , SimulationSystem SimSystem)
        {
            dataGridView1.DataSource = SimOutput.CurrentSimulation;
            dataGridView3.DataSource = SimOutput.CurrentSimulationPerformance;
            dataGridView2.DataSource = SimOutput.ProposedSimulation;
            dataGridView4.DataSource = SimOutput.ProposedSimulationPerformance;
            if(SimSystem.CurrentPerformanceMeasures.TotalCost < SimSystem.ProposedPerformanceMeasures.TotalCost) MessageBox.Show("The current method is better than the proposed method.");
            else if (SimSystem.CurrentPerformanceMeasures.TotalCost > SimSystem.ProposedPerformanceMeasures.TotalCost)  MessageBox.Show("The proposed method is better than the current method.");
            else MessageBox.Show("The two methods have the same efficiency.");
        }
    }
}
