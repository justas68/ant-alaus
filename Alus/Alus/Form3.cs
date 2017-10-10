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
using GoogleApi.Entities;

namespace Alus
{

    public partial class Form3 : Form
    {
        Location location = new Alus.Location();
        Image<Bgr, byte> img;
        int zoom = 12;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            location.findLocation();
            Double lat = location.Lat;
            Double lon = location.Lon;
            while (lat.ToString() == "NaN")
            {
                location.findLocation();
                lat = location.Lat;
                lon = location.Lon;
            }
            double lat2 = lat;
            double lon2 = lon;
            lat2 -= 0.1;
            string latlng = lat.ToString().Replace(",", ".") + "," + lon.ToString().Replace(",", ".");
            string latlng2 = lat2.ToString().Replace(",", ".") + "," + lon2.ToString().Replace(",", ".");
            string path = "https://maps.googleapis.com/maps/api/staticmap?center=" +  latlng+ "&zoom=" + zoom.ToString() + "&size=400x400&markers=color:blue%7Clabel:S%7C" + latlng + "&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
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
            if (ModifierKeys.HasFlag(Keys.Control))
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
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
            this.Close();
        }

    }
}
