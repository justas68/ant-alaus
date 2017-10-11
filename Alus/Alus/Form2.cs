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
    public partial class Form2 : Form
    {
        string cityName;

        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cityName = textBox1.Text;
            try
            {
                StringBuilder address = new StringBuilder();
                address.Append("http://maps.google.com/maps?q=");
                address.Append(cityName + " barai");
                webBrowser1.Navigate(address.ToString());

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error");
            }
        }
        
    }
}
