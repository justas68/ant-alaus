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
using Alus.Core.Models;

namespace Alus
{
    public partial class StatisticalTableForm : ChildForm
    {
        private readonly IBarContainer _barContainer;

        private readonly NearestBars nearestBars;

        public StatisticalTableForm(IBarContainer barContainer, NearestBars nearestBars)
        {
            InitializeComponent();
            _barContainer = barContainer;
            this.nearestBars = nearestBars;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StaticticalTable_Load(object sender, EventArgs e)
        {
            var evaluations = _barContainer.GetAll();

            var newBarList = nearestBars.FindBars().Select(bar =>
            {
                var secondBar = evaluations.Where(b => b.PlaceId == bar.PlaceId).FirstOrDefault();

                return new Bar(
                    bar.Name,
                    bar.Coordinates,
                    bar.OnlineRating,
                    bar.Address,
                    bar.PlaceId,
                    secondBar?.Evaluation,
                    (secondBar == null ? 0.0 : secondBar.Percentage),
                    bar.BeersBought
                );
            });

            AddToTable(newBarList.ToList());

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

        private void AddToTable(List<Bar> barList)
        {
            foreach (Bar bar in barList)
            {
                int rowIndex = this.dataGridView1.Rows.Add();
                var row = this.dataGridView1.Rows[rowIndex];
                row.Cells[1].Value = bar.Name;
                row.Cells[2].Value = bar.Address;
                row.Cells[3].Value = bar.OnlineRating;
                try
                {
                    if (nearestBars.FindBarWorkingTime(bar.PlaceId).IsOpenNow == true)
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
                if (bar.Evaluation != null)
                {
                    row.Cells[5].Value = bar.Evaluation;
                }
                else
                {
                    row.Cells[5].Value = "-";
                }
                if (bar.Percentage == 0)
                {
                    row.Cells[6].Value = "-";
                }
                else
                {
                    row.Cells[6].Value = Math.Round(bar.Percentage, 2) + "%";
                }
            }
        }

    }
}
