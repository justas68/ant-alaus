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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "c:\\";
            open.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            open.FilterIndex = 2;
            open.RestoreDirectory = true;

            if (open.ShowDialog() == DialogResult.OK)
            {
                string file = open.FileName;
                pictureBox1.Load(file);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
