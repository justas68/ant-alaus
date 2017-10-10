using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Alus
{
    public partial class Form1 : Form
    {
        int top, mid, bot;
        Image<Bgr, byte> img;
        int point = 0; // pasako, į kurį image žiūriu programa
        List<String> sarasas;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var open = new FolderBrowserDialog())
            {
                DialogResult result = open.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(open.SelectedPath))
                {
                    point = 0;
                }
                string[] files = Directory.GetFiles(open.SelectedPath);
                sarasas = new List<string>(files);
                sarasas = sarasas.Where(path => path.ToLower().EndsWith(".jpg")).ToList();
                if (sarasas.Count() == 0)
                {
                    sarasas = null;
                    {
                        return;
                    }
                }
                img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
                imageBox1.Image = img;

            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (sarasas == null)
            {
                return;
            }
            point--;

            if (!sarasas.InRange(point))
            {
                point = sarasas.Count() - 1;
            }

            img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
            imageBox1.Image = img;
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (sarasas == null)
            {
                return;
            }
            point++;

            if (!sarasas.InRange(point))
            {
                point = 0;
            }

            img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);

            imageBox1.Image = img;
        }

        private bool CheckBounds(int x1, int x2, int x)
        {
            return Enumerable.Range(x1, x2).Contains(x);
        }

        private bool CheckBounds<T>(List<T> list, int x)
        {
            return CheckBounds(0, list.Count(), x);
        }

        private void ToGray_Click(object sender, EventArgs e)
        {
            if (sarasas == null)
            {
                return;
            }
            img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
            Image<Gray, byte> grayImage;
            grayImage = new Image<Gray, byte>(img.Width, img.Height, new Gray(0));
            grayImage = img.Canny(50, 100);

            imageBox1.Image = grayImage;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sarasas == null)
            {
                return;
            }
            img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
            Image<Gray, float> sobelImage;
            Image<Gray, byte> grayImage = img.Convert<Gray, byte>();
            sobelImage = grayImage.Sobel(0, 1, 3);

            imageBox1.Image = sobelImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sarasas == null)
            {
                return;
            }
            img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
            Image<Gray, float> laplaceImage;
            Image<Gray, byte> grayImage = img.Convert<Gray, byte>();
            laplaceImage = grayImage.Laplace(3);

            imageBox1.Image = laplaceImage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            img = new Image<Bgr, byte>(sarasas.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
            Image<Gray, byte> grayImage = img.Convert<Gray, byte>();
        }

        
    }
}
