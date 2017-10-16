using Alus.GoogleApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alus
{
    public partial class StatisticalTableForm : Form
    {
        private NearestBars nearestBars = new NearestBars();
        private BarEvaluationReader reader = new BarEvaluationReader();

        public StatisticalTableForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Close();
        }

        private void StaticticalTable_Load(object sender, EventArgs e)
        {
            foreach (Bar baras in nearestBars.Location())
            {
                int rowIndex = this.dataGridView1.Rows.Add();
                var row = this.dataGridView1.Rows[rowIndex];
                row.Cells[1].Value = baras.Name;
                row.Cells[2].Value = baras.Address;
                if (baras.Rating != 0)
                {
                    row.Cells[3].Value = baras.Rating;
                }
                else
                {
                    row.Cells[3].Value = "-";
                }
                try
                {
                    if (baras.Hour.IsOpenNow == true)
                    {
                        row.Cells[4].Value = "OPEN";
                    }
                    else
                    {
                        row.Cells[4].Value = "CLOSED";
                    }
                }
                catch (Exception)
                {
                    row.Cells[4].Value = "No work time";
                }
            }
            foreach (VisitedBars baras in reader.ReadFile())
            {
                int rowIndex = this.dataGridView1.Rows.Add();
                var row = this.dataGridView1.Rows[rowIndex];
                row.Cells[1].Value = baras.Name;
                row.Cells[5].Value = baras.Rating;
            }
            Numbering();
        }

        private void Numbering()
        {
            int rownum = 0;
            foreach (DataGridViewRow rows in dataGridView1.Rows)
            {
                dataGridView1.Rows[rownum].Cells[0].Value = rownum + 1;
                rownum += 1;
            }
            dataGridView1.AllowUserToAddRows = false;
        }
    }
}
