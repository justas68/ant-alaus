using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Reflection;
using System.IO;

namespace Alus
{
    public partial class EvaluationForm : Form
    {
        private EvaluationClass evac;
        private bool _newBar;
        private double percentages;
        public EvaluationForm()
        {
            InitializeComponent();
            evac = new EvaluationClass();
            _newBar = false;
        }

        public EvaluationForm(double percentages)
        {
            InitializeComponent();
            _newBar = true;
            evac = new EvaluationClass();
            this.percentages = percentages;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (evac.SelectedItemCheck(listBox1, textBox2, _newBar))
            {
                Console.Write(listBox1.SelectedIndex);
                if (!_newBar)
                {
                    textBox1.Text = evac.barList.ElementAt(listBox1.SelectedIndex).Evaluation;
                }
                else
                {
                    textBox1.Text = null;
                }
            }
            else
            {
                MessageBox.Show("No bar here");
            }
        }

        private void EvaluationForm_Load(object sender, EventArgs e)
        {
            evac.RedrawList(listBox1, _newBar);
        }

        private void evaluateButton_Click(object sender, EventArgs e)
        {
            evac.EvaluationButtonSet(textBox2, listBox1, trackBar1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Hide();
        }

        private void changeEvaluationButton_Click(object sender, EventArgs e)
        {
            evac.ChangeEvaluation(textBox2, listBox1, trackBar1);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = "" + trackBar1.Value;
        }
    }
}
