using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alus
{
    public partial class MainForm : Form
    {
        EvaluationForm brv = new EvaluationForm();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            brv.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new ImageRecognitionForm()).Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new LocationForm()).Show();
             this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void suggestions_Click(object sender, EventArgs e)
        {
            (new FeedbackForm()).Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (new StatisticalTable()).Show();
            this.Hide();
        }
    }
}
