using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MultiChannelQueueModels
{
    public partial class Form1 : Form
    {
        public List<Server> Servers;
        public List<TimeDistribution> InterarrivalTime;
        int s = 1 , n=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlservers.Visible = false;
            label2.Visible = false;
            Servers = new List<Server>();
            InterarrivalTime = new List<TimeDistribution>();
        }

        private void btnservers_Click(object sender, EventArgs e)
        {
          
            n += Int32.Parse(txtnservers.Text.ToString());
            if (n>0)
            {
                pnlservers.Visible = true;
                label2.Visible = true;
                Servers.Clear();
                if(s==1)
                Servers = new List<Server>();
                
              //  s++;
              //  MessageBox.Show(s.ToString());
                label2.Text = "Server Number: " + s;
            }
        }
        private void Calc_Ranges(List<TimeDistribution> server)
        {
            double CurEnd = 0;
            double CurCum = 0;
            for(int i=0; i<server.Count; i++)
            {
                server[i].CummProbability = CurCum + server[i].Probability;
                CurCum = server[i].CummProbability;
                server[i].MinRange = CurEnd + 1;
                server[i].MaxRange = CurEnd + server[i].Probability*100;
                CurEnd = server[i].MaxRange;
            }
        }

        private void btninterarrival_Click(object sender, EventArgs e)
        {
            pnlservers.Visible = true;
            InterarrivalTime.Clear();
            InterarrivalTime = new List<TimeDistribution>();
        }

        //for test 
        /*private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Servers.Count(); ++i) {
                MessageBox.Show(Servers[i].ServerId.ToString());
                for (int j = 0; j < Servers[i].ServiceTimeDistribution.Count(); ++j)
                {
                    MessageBox.Show(Servers[i].ServiceTimeDistribution[j].Time.ToString());
                    MessageBox.Show(Servers[i].ServiceTimeDistribution[j].Probability.ToString());
                }
                }
        }*/

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (label2.Visible == true)
            {
                if (s <= n)
                {
                    int m = dataGridView2.Rows.Count;
                    List<TimeDistribution> tmptimedis = new List<TimeDistribution>();
                    double cumlative = 0;
                    for (int i = 0; i < m - 1; ++i)
                    {
                        cumlative += Math.Round(float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 5);
                    }
                    if (cumlative == 1)
                    {
                        for (int i = 0; i < m - 1; ++i)
                        {
                            tmptimedis.Add(new TimeDistribution());
                            //if (dataGridView2.Rows[i].Cells[0].Value == null) MessageBox.Show(m.ToString());
                            tmptimedis[i].Time = Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString());
                            tmptimedis[i].Probability = Math.Round(float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 5);
                        }
                        Servers.Add(new Server());
                        Servers[s - 1].ServerId = s;
                        // Servers[s - 1].Name = "as";
                        Servers[s - 1].ServiceEfficiency = Math.Round(double.Parse(txtefficiency.Text.ToString()), 5);
                        Servers[s - 1].ServiceTimeDistribution = tmptimedis;

                        Calc_Ranges(Servers[s - 1].ServiceTimeDistribution);
                        dataGridView2.Rows.Clear();
                        s++;
                        label2.Text = "Server Number: " + s;
                    }
                    else { MessageBox.Show("cumulative probability is NOT equal 1"); }
                }
                if (s > n)
                {
                    pnlservers.Visible = false;
                    label2.Visible = false;
                }
            }
            else
            {
                int m = dataGridView2.Rows.Count;
                for (int i = 0; i < m - 1; i++)
                {
                    InterarrivalTime.Add(new TimeDistribution(Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString()), Math.Round(float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 3)));
                }
                Calc_Ranges(InterarrivalTime);
                dataGridView2.Rows.Clear();
                pnlservers.Visible = false;
            }
        }
    }
}
