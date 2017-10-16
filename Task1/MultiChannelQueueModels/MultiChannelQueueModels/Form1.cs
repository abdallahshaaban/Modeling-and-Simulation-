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
        public List<List<TimeDistribution>> Servers;
        public List<TimeDistribution> InterarrivalTime;
        int s = 0 , n=0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pnlservers.Visible = false;
            label2.Visible = false;
            Servers = new List<List<TimeDistribution>>();
            InterarrivalTime = new List<TimeDistribution>();
        }

        private void btnservers_Click(object sender, EventArgs e)
        {
            n = Int32.Parse(txtnservers.Text.ToString());
            if (n!=0)
            {
                pnlservers.Visible = true;
                label2.Visible = true;
                Servers.Clear();
                Servers = new List<List<TimeDistribution>>();
                s++;
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

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (label2.Visible == true)
            {
                if (s <= n)
                {
                    int m = dataGridView2.Rows.Count;
                    Servers.Add(new List<TimeDistribution>());
                    for (int i = 0; i < m - 1; i++)
                    {
                        Servers[s - 1].Add(new TimeDistribution(Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString()), Math.Round(float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 3), 0, 0, 0));
                    }
                    Calc_Ranges(Servers[s - 1]);
                    dataGridView2.Rows.Clear();
                    s++;
                    label2.Text = "Server Number: " + s;
                }
                if (s > n)
                {
                    s = 0;
                    n = 0;
                    pnlservers.Visible = false;
                    label2.Visible = false;
                }
            }
            else
            {
                int m = dataGridView2.Rows.Count;
                for (int i = 0; i < m - 1; i++)
                {
                    InterarrivalTime.Add(new TimeDistribution(Int32.Parse(dataGridView2.Rows[i].Cells[0].Value.ToString()), Math.Round(float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()), 3), 0, 0, 0));
                }
                Calc_Ranges(InterarrivalTime);
                dataGridView2.Rows.Clear();
                pnlservers.Visible = false;
            }
        }
    }
}
