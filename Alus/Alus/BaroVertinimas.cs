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
    public partial class BaroVertinimas : Form
    {
        List<String> bars = new List<String>();
        List<String> ranks = new List<String>();

        public BaroVertinimas()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = (ranks.ElementAt(listBox1.SelectedIndex));
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BaroVertinimas_Load(object sender, EventArgs e)
        {
            redraw_Bar_List();
        }

        private void redraw_Bar_List() {
            
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "Alus.BaruVertinimai.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                int i = 0;
                string st;
                st = reader.ReadLine();
                while ((st = reader.ReadLine()) != null)
                {
   
                    var values = st.Split(',');
                    bars.Add(values[0]);
                    ranks.Add(values[1]);
                }
            }

            for (int i = 0; i < bars.Count(); i++)
            {
                listBox1.Items.Add(bars.ElementAt(i));
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = "" + trackBar1.Value;
        }

        public bool evaluation_check(int eva) {

            if (eva <= 10 && eva >= 1)
                return true;
            else return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName + "\\BaruVertinimai.txt";
            if (File.Exists(path)) {
                using (TextWriter sw = new StreamWriter(path, true))
                {
                    if (evaluation_check(trackBar1.Value))
                    {
                        sw.WriteLine(textBox2.Text.ToString() + "," + trackBar1.Value.ToString());
                        bars.Add(textBox2.Text);
                        ranks.Add(trackBar1.Value.ToString());
                        listBox1.Items.Add(textBox2.Text);
                    }
                    else MessageBox.Show("Bad evaluation input");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
            this.Close();
        }
    }
}
