using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alus
{
    class EvaluationClass
    {
        public List<String> _bars = new List<String>();
        public List<String> _ranks = new List<String>();

        private static string resourceName = "BarEvaluation.txt";

        public EvaluationClass()
        {
            ReadEvaluationFile();
        }

        private void ReadEvaluationFile()
        {
            using (StreamReader fileReader = new StreamReader(resourceName))
            {
                string st = fileReader.ReadLine();
                while ((st = fileReader.ReadLine()) != null)
                {
                    var stringValues = st.Split(',');
                    _bars.Add(stringValues[0]);
                    _ranks.Add(stringValues[1]);
                }
            }
        }

        public bool SelectedItemCheck(ListBox listBox)
        {
            return listBox.SelectedIndex < _ranks.Count && listBox.SelectedIndex > -1;
        }

        public void RedrawList(ListBox listBox)
        {
            for (int i = 0; i < _bars.Count(); i++)
            {
                listBox.Items.Add(_bars.ElementAt(i));
            }
        }

        public void EvaluationButtonSet(TextBox textBox, ListBox listBox, TrackBar trackBar)
        {
            bool inList = false;
            foreach (var barList in _bars)
            {
                if (barList == textBox.Text)
                {
                    inList = true;
                }
            }

            if (File.Exists(resourceName) && inList == false)
            {
                using (TextWriter streamWriter = new StreamWriter(resourceName, true))
                {
                    if (EvaluationCheck(trackBar.Value))
                    {
                        streamWriter.WriteLine(textBox.Text.ToString() + "," + trackBar.Value.ToString());
                        _bars.Add(textBox.Text);
                        _ranks.Add(trackBar.Value.ToString());
                        listBox.Items.Add(textBox.Text);
                    }
                    else
                    {
                        MessageBox.Show("Bad evaluation input");
                    }
                }
            }
        }

        public bool EvaluationCheck(int evaluation)
        {
            return (evaluation <= 10 && evaluation >= 1);
        }

        public void ChangeEvaluation(TextBox textBox, ListBox listBox, TrackBar trackBar)
        {
            _ranks[listBox.SelectedIndex] = "" + trackBar.Value;
            if (File.Exists(resourceName))
            {
                using (TextWriter streamWriter = new StreamWriter(resourceName, false))
                {
                    for (int i = 0; i < _ranks.Count; i++)
                    {
                        streamWriter.WriteLine(_bars[i] + "," + _ranks[i]);
                    }
                }
            }
        }
    }
}
