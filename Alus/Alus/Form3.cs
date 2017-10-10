using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alus
{

    public partial class Form3 : Form
    {
        private List<Baras> _barai;
        private Location _location = new Alus.Location();
        private Image<Bgr, byte> _image;
        private int _zoom = 12;
        private int _count = 0;
        private bool _ieskoti = true;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _count = 0;
            _location.FindLocation();
            Double lat = _location.Lat;
            Double lon = _location.Lon;
            while (lat.ToString() == "NaN")
            {
                _location.FindLocation();
                lat = _location.Lat;
                lon = _location.Lon;
            }
            String path;
            double lat2 = lat;
            double lon2 = lon;
            lat2 -= 0.1;
            if (_ieskoti == true)
            {
                textBox1.AppendText("0 - Jūsų buvimo vieta " + Environment.NewLine);
                _barai = new List<Baras>();
            }
            string latlng = lat.ToString().Replace(",", ".") + "," + lon.ToString().Replace(",", ".");
            string latlng2 = lat2.ToString().Replace(",", ".") + "," + lon2.ToString().Replace(",", ".");
            if (_ieskoti == true)
            {
                path = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latlng + "&rankby=distance&type=bar&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
                using (WebClient wc = new WebClient())
                {
                    wc.DownloadFile(path, "lol.txt");
                }
                FindBars();
            }
            path = "https://maps.googleapis.com/maps/api/staticmap?center=" +  latlng+ "&zoom=" + _zoom.ToString() + "&size=400x400&markers=color:blue%7Clabel:0%7C" + latlng;
            foreach(Baras baras in _barai) {
                _count++;
                path = path + "&markers=color:blue%7Clabel:" + _count.ToString() + "%7C" + baras.getCords();
                if (_ieskoti == true)
                {
                    textBox1.AppendText(_count.ToString() + " - " + baras.getPav() + Environment.NewLine);
                }
            }
            path = path + "&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(path, "lol.png");
                _image = new Image<Bgr, byte>("lol.png");
                pictureBox1.Image = _image.Bitmap;
            }
            path = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + latlng + "&rankby=distance&type=bar&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(path, "lol.txt");
            }
            
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                if (e.Delta > 0)
                {
                    if (_zoom == 20)
                    {
                        return;
                    }
                    _zoom++;
                }
                else
                {
                    if (_zoom == 0)
                    {
                        return;
                    }
                    _zoom--;
                }
                _ieskoti = false;
                button1.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new Form2()).Show();
            this.Close();
        }
        private void FindBars()
        {
            String lat;
            String lon;
            const Int32 BufferSize = 128;
            using (var fileStream = File.OpenRead("lol.txt"))
            using (var reader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
            {
                int i = 0;
                string st;
                st = reader.ReadLine();
                while ((st = reader.ReadLine()) != null)
                {
                    while (!(st.Contains("location")))
                        {
                        st = reader.ReadLine();
                        if (st == null)
                        {
                            break;
                        }
                    }
                    if (st == null)
                    {
                        break;
                    }
                    lat = reader.ReadLine();
                    lat = Regex.Replace(lat, "[^0-9.]", "");
                    lon = reader.ReadLine();
                    lon = Regex.Replace(lon, "[^0-9.]", "");
                    while (!(st.Contains("name")))
                    {
                        st = reader.ReadLine();
                        if (st == null)
                        {
                            break;
                        }
                    }
                    if (st == null)
                    {
                        break;
                    }
                    st = st.Replace(" ", "").Replace("\"", "").Replace("\\", "").Replace(":", "").Replace(",", "").Replace("name", "");
                    _barai.Add(new Baras(st, lat + "," + lon));
                }
            }
        }

    }
}
