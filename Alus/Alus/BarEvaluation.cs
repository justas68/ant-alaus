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
    public partial class BarEvaluation : Form
    {
        List<String> _bars = new List<String>();
        List<String> _ranks = new List<String>();

        public BarEvaluation()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (_ranks.ElementAt(listBox1.SelectedIndex));
        }

        private void BarEvaluation_Load(object sender, EventArgs e)
        {
            RedrawBarList();
        }

        private void RedrawBarList() {
            
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Alus.BarEvaluation.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string st;
                st = reader.ReadLine();
                while ((st = reader.ReadLine()) != null)
                {
   
                    var values = st.Split(',');
                    _bars.Add(values[0]);
                    _ranks.Add(values[1]);
                }
            }

            for (int i = 0; i < _bars.Count(); i++)
            {
                listBox1.Items.Add(_bars.ElementAt(i));
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = "" + trackBar1.Value;
        }

        public bool EvaluationCheck(int evaluation) {

            return (evaluation <= 10 && evaluation >= 1) ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\BaruVertinimai.txt";
            if (File.Exists(path)) {
                using (TextWriter sw = new StreamWriter(path, true))
                {
                    if (EvaluationCheck(trackBar1.Value))
                    {
                        sw.WriteLine(textBox2.Text.ToString() + "," + trackBar1.Value.ToString());
                        _bars.Add(textBox2.Text);
                        _ranks.Add(trackBar1.Value.ToString());
                        listBox1.Items.Add(textBox2.Text);
                    }
                    else MessageBox.Show("Bad evaluation input");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
            this.Close();
        }
    }
}
