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
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EvaluationForm brv = new EvaluationForm();
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
    }
}
