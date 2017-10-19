using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;


namespace Alus
{
    public partial class ImageRecognitionForm : Form
    {
        Location location = new Alus.Location();
        Point[] p1 = new Point[3];  //liniju pirmas taskas
        Point[] p2 = new Point[3];  //liniju antras taskas
        int eilCount = 0; // pasako, kiek liniju uzbrezta
        Image<Bgr, byte> img;
        int point = 0; // pasako, į kurį image žiūriu programa
        List<String> list;
        public static double percentage;
        public ImageRecognitionForm()
        {
            InitializeComponent();
        }
        private double proc;
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
                list = new List<string>(files);
                list = list.Where(path => path.ToLower().EndsWith(".jpg")).ToList();
                if (list.Count() == 0)
                {
                    list = null;
                    {
                        return;
                    }
                }
                img = new Image<Bgr, byte>(list.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
                pictureBox1.Image = img.Bitmap;
            }
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (list == null)
            {
                return;
            }
            point--;
            eilCount = 0;
            p1 = new Point[3];
            p2 = new Point[3];
            pictureBox1.Invalidate();
            if (!list.InRange(point))
            {
                point = list.Count() - 1;
            }

            img = new Image<Bgr, byte>(list.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);
            pictureBox1.Image = img.Bitmap;
        }

        private void Next_Click(object sender, EventArgs e)
        { 
            if (list == null)
            {
                return;
            }
            point++;
            eilCount = 0;
            p1 = new Point[3];
            p2 = new Point[3];
            pictureBox1.Invalidate();
            if (!list.InRange(point))
            {
                point = 0;
            }

            img = new Image<Bgr, byte>(list.ElementAt(point)).Resize(760, 500, Emgu.CV.CvEnum.Inter.Linear, true);

            pictureBox1.Image = img.Bitmap;
        }

        private bool CheckBounds(int x1, int x2, int x)
        {
            return Enumerable.Range(x1, x2).Contains(x);
        }

        private bool CheckBounds<T>(List<T> list, int x)
        {
            return CheckBounds(0, list.Count(), x);
        }

        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (eilCount < 3)
            {
                p1[eilCount] = e.Location;
            }
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (eilCount < 3)
                {
                    p2[eilCount] = e.Location;
                    pictureBox1.Invalidate();
                }
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            

        }
        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (eilCount < 3)
            {
                p2[eilCount] = e.Location;
                pictureBox1.Invalidate();
                eilCount++;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                if ((p1[i] != null) && (p2[i] != null))
                {
                    e.Graphics.DrawLine(new Pen(Color.Red), p1[i], p2[i]);
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (eilCount != 3)
            {
                MessageBox.Show("Firstly, set lines, which mark glass top, fill level and bottom");
                return;
            }
            Calculator calc = new Calculator();
            proc = calc.Percentage(p1, p2);
            if (proc == 0)
            {
                MessageBox.Show("This shape of glass is not allowed");
                eilCount = 0;
                p1 = new Point[3];
                p2 = new Point[3];
                pictureBox1.Invalidate();
                return;
            }
            percetage = Math.Round(proc, 2);
            MessageBox.Show("Filled up : " + Math.Round(proc, 2).ToString() + "%");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            (new MainForm()).Show();
        }

        private void addBar_Click(object sender, EventArgs e)
        {
            if (proc == 0)
            {
                MessageBox.Show("First determine how much beer was poured in!");
            }
            else
            {
                this.Close();
                (new EvaluationForm(proc)).Show();
            }
        }
    }
}
