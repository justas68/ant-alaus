using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
namespace Alus
{
    public partial class EvaluationForm : Form
    {
        private bool _newBar;
        private double _percentages;
        private NearestBars _nearestBars = new NearestBars();
        private List<Bar> _nearestBarList;
        private List<Bar> _barList = new List<Bar>();
        private ReadAndWriteFromFile _readerWriter = new ReadAndWriteFromFile();
        private bool _choice;
        public EvaluationForm()
        {
            InitializeComponent();
            _newBar = false;
            _barList = _readerWriter.ReadFile();
            _nearestBarList = _nearestBars.FindBars();
            evaluateButton.Visible = false;
            notThisBar.Visible = false;
        }

        public EvaluationForm(double percentages)
        {
            InitializeComponent();
            _newBar = true;
            this._percentages = percentages;
            changeEvaluationButton.Visible = false;
            deleteBar.Visible = false;
            _barList = _readerWriter.ReadFile();
            _nearestBarList = _nearestBars.FindBars();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_newBar)
            {
                textBox2.Text = _nearestBarList.ElementAt(listBox1.SelectedIndex).Name;
                _choice = listBox1.SelectedIndex < _nearestBarList.Count && listBox1.SelectedIndex > -1;
            }
            else
            {
                textBox2.Text = _barList.ElementAt(listBox1.SelectedIndex).Name;
                _choice = listBox1.SelectedIndex < _barList.Count && listBox1.SelectedIndex > -1;
            }
            if (_choice)
            {
                Console.Write(listBox1.SelectedIndex);
                if (!_newBar)
                {
                    var bar = _barList.ElementAt(listBox1.SelectedIndex);
                    trackBar1.Value = int.Parse(bar.Evaluation);
                    textBox1.Text = bar.Evaluation;
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
            if (_newBar)
            {
                listBox1.Items.Add(_nearestBarList.ElementAt(0).Name);
                textBox2.Text = _nearestBarList.ElementAt(0).Name;
            }
            else
            {
                foreach (Bar bar in _barList)
                {
                    listBox1.Items.Add(bar.Name);
                }
            }
        }

        private void evaluateButton_Click(object sender, EventArgs e)
        {
            _choice = false;
            foreach (Bar bar in _barList)
            {
                if (bar.PlaceId == _nearestBarList.ElementAt(listBox1.SelectedIndex).PlaceId)
                {
                    _choice = true;
                }
            }

            if (_choice == false)
            {
                if (EvaluationCheck(trackBar1.Value))
                {
                    Bar bar = _nearestBarList.ElementAt(listBox1.SelectedIndex);
                    bar.Evaluation = trackBar1.Value.ToString();
                    bar.Percentage = (bar.BeersBought * bar.Percentage + Math.Round(_percentages, 2)) / (bar.BeersBought + 1);
                    bar.BeersBought++;
                    _readerWriter.WriteLineToFile(bar);
                    _barList.Add(bar);
                    listBox1.Items.Add(textBox2.Text);
                    _choice = true;
                }
                else
                {
                    MessageBox.Show("Bad evaluation input");
                    _choice = false;
                }
            }
            else
            {
                _choice = false;
            }
            if (_newBar && _choice)
            {
                MessageBox.Show("New bar added");
                this.Hide();
                (new MainForm()).Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Hide();
        }

        private void changeEvaluationButton_Click(object sender, EventArgs e)
        {
            _barList.ElementAt(listBox1.SelectedIndex).Evaluation = "" + trackBar1.Value.ToString();
            _readerWriter.WriteListToFile(_barList);
            MessageBox.Show("Evaluation changed");
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            textBox1.Text = "" + trackBar1.Value;
        }

        public bool EvaluationCheck(int evaluation)
        {
            bool check = evaluation <= 10 && evaluation >= 1;
            return check;
        }

        private void deleteBar_Click(object sender, EventArgs e)
        {
            _barList.RemoveAt(listBox1.SelectedIndex);
            _readerWriter.WriteListToFile(_barList);
            MessageBox.Show("Bar was deleted");
            textBox1.Text = null;
            textBox2.Text = null;
            listBox1.Items.Clear();
            EvaluationForm_Load(sender, e);
        }

        private void notThisBar_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox1.Text = null;
            textBox2.Text = null;
            foreach (Bar bar in _nearestBarList.Skip(1))
            {
                listBox1.Items.Add(bar.Name);
            }
            notThisBar.Visible = false;
        }
    }
}
