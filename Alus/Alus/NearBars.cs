using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alus
{

    public partial class NearBars : Form
    {
        Location location = new Alus.Location();
        Image<Bgr, byte> img;
        private Button button1;
        int zoom = 12;
        public NearBars()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            location.findLocation();
            Double lat = location.Lat;
            Double lon = location.Lon;
            string latlng = lat.ToString().Replace(",", ".") + "," + lon.ToString().Replace(",", ".");
            string path = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlng +
               "&zoom=" + zoom.ToString() + "&size=400x400&markers=color:blue%7Clabel:S%7C" + latlng + "&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
            using (WebClient wc = new WebClient())

            {
                // SetWebBrowserVersion(11001);
                wc.DownloadFile(path, "lol.png");
                img = new Image<Bgr, byte>("lol.png");
                pictureBox1.Image = img.Bitmap;
            }
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (zoom == 20)
                {
                    return;
                }
                zoom++;
            }
            else
            {
                if (zoom == 0)
                {
                    return;
                }
                zoom--;
            }
            button1.PerformClick();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
            this.Close();
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(100, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(145, 45);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NearBars
            // 
            this.ClientSize = new System.Drawing.Size(526, 413);
            this.Controls.Add(this.button1);
            this.Name = "NearBars";
            this.ResumeLayout(false);

        }
    }
}
