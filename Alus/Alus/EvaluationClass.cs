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
        public NearestBars nearestBars = new NearestBars();
        public List<Bar> nearestBarList;
        public List<Bar> barList = new List<Bar>();
        private int index ;
        private static string resourceName = "BarEvaluation.txt";

        public EvaluationClass()
        {
            ReadEvaluationFile();
            nearestBarList = nearestBars.Location();
        }

        private void ReadEvaluationFile()
        {
            using (StreamReader fileReader = new StreamReader(resourceName))
            {
                string st = fileReader.ReadLine();
                while ((st = fileReader.ReadLine()) != null)
                {
                    var stringValues = st.Split(';');
                    barList.Add(new Bar(stringValues[0], stringValues[1], Convert.ToDouble(stringValues[2]), stringValues[3], stringValues[4], stringValues[5]));
                }
            }
        }

        public bool SelectedItemCheck(ListBox listBox, TextBox textBox, bool choice)
        {
            index = listBox.SelectedIndex;
            if (choice)
            {
                textBox.Text = nearestBarList.ElementAt(listBox.SelectedIndex).Name;
                return listBox.SelectedIndex < nearestBarList.Count && listBox.SelectedIndex > -1;
            }
            else
            {
                textBox.Text = barList.ElementAt(listBox.SelectedIndex).Name;
                return listBox.SelectedIndex < barList.Count && listBox.SelectedIndex > -1;
            }
        }

        public void RedrawList(ListBox listBox, bool choice)
        {
            if (choice)
            {
                foreach (Bar bar in nearestBarList)
                {
                    listBox.Items.Add(bar.Name);
                }
            }
            else
            {
                foreach (Bar bar in barList)
                {
                    listBox.Items.Add(bar.Name);
                }
            }
        }

        public void EvaluationButtonSet(TextBox textBox, ListBox listBox, TrackBar trackBar)
        {
            bool inList = false;
            foreach (Bar bar in barList)
            {
                if (bar.PlaceId == nearestBarList.ElementAt(index).PlaceId)
                {
                    inList = true;
                    MessageBox.Show("Such a bar is already written");
                }
            }

            if (File.Exists(resourceName) && inList == false)
            {
                using (var streamWriter = new StreamWriter(resourceName, true))
                {
                    if (EvaluationCheck(trackBar.Value))
                    {
                        try
                        {
                            Bar bar = nearestBarList.ElementAt(index);
                            streamWriter.WriteLine(bar.Name + ";" + bar.Coordinates + ";" + bar.OnlineRating + ";" + bar.Address + ";" + bar.PlaceId + ";" + trackBar.Value.ToString());
                            bar.Evaluation = trackBar.Value.ToString();
                            barList.Add(bar);
                            listBox.Items.Add(textBox.Text);

                        }
                        catch(Exception)
                        {
                            MessageBox.Show(" Please reenter the information.");
                        }
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
            index = listBox.SelectedIndex;
            barList.ElementAt(index).Evaluation = "" + trackBar.Value.ToString();
            if (File.Exists(resourceName))
            {
                using (TextWriter streamWriter = new StreamWriter(resourceName, false))
                {
                    foreach(Bar bar in barList)
                    {
                        streamWriter.WriteLine(bar.Name + ";" + bar.Coordinates + ";" + bar.OnlineRating + ";" + bar.Address + ";" + bar.PlaceId + ";" + trackBar.Value.ToString());
                    }
                }
            }
        }
    }
}
