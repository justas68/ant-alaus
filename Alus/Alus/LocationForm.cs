using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
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

    public partial class LocationForm : Form
    {
        private bool _isDown = false;
        private double _cordChange1 = 0;
        private double _cordChange2 = 0;
        private List<Bar> _barai;
        private Location _location = new Alus.Location();
        private Image<Bgr, byte> _image;
        private int _zoom = 12;
        private bool _ieskoti = true;
        private bool _ctrl = false;

        private double lat;
        private double lon;
        private double lat2;
        private double lon2;

        // use coordinates of faculty campus as default location
        private static Location defaultLocation = new Alus.Location(54.729714d, 25.263445d);

        public LocationForm()
        {
            InitializeComponent();
            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;
        }

        private Stream GetStreamFromUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                return new MemoryStream(wc.DownloadData(url));
            }
        }

        private Stream NearbySearch(Location location)
        {
            string path = "https://maps.googleapis.com/maps/api/place/nearbysearch/json?location=" + location + "&rankby=distance&type=bar&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU";
            return GetStreamFromUrl(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _location = Alus.Location.FindLocation(3, defaultLocation);

            lat = lat2 = _location.Latitude;
            lon = lon2 = _location.Longtitude;

            String path;
            if (_ieskoti == true)
            {
                listBox1.Items.Add("* - Your location");
                _barai = new List<Bar>();
            }
            string latlng = _location.ToString();

            if (_ieskoti == true)
            {
                using (var ms = NearbySearch(_location))
                {
                    _barai.AddRange(FindBars(ms).ToList());
                }
            }

            path = "https://maps.googleapis.com/maps/api/staticmap?center=" + latlng + "&zoom=" + _zoom.ToString() + "&size=400x400&markers=color:blue%7Clabel:*%7C" + latlng;
            int count = 'A';
            foreach (Bar baras in _barai)
            {
                path = path + "&markers=color:blue%7Clabel:" + (char)count + "%7C" + baras.Coordinates;
                if (_ieskoti == true)
                {
                    listBox1.Items.Add((char)count + " - " + baras.Name);
                }
                count++;
            }

            path = path + "&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU"; // API key

            using (WebClient wc = new WebClient())
            {
                wc.DownloadFile(path, "lol.png");
                _image = new Image<Bgr, byte>("lol.png");
                pictureBox1.Image = _image.Bitmap;
            }
            _ieskoti = false;
        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_ctrl)
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
                button1.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new MainForm()).Show();
            this.Close();
        }

        private IEnumerable<Bar> FindBars(Stream stream)
        {
            String lat;
            String lon;
            using (var reader = new StreamReader(stream))
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
                    st = st.Replace("   ", "").Replace("  ", "").Replace("\"", "").Replace("\\", "").Replace(":", "").Replace(",", "").Replace("name", "");
                    yield return new Bar(st, lat + "," + lon);
                }
            }
        }

        private void Form3_KeyDown(object sender, KeyEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                _ctrl = true;
                return;
            }

            if (e.KeyCode == Keys.W)
            {
                _isDown = true;
                _cordChange1 = 0.004;
                _cordChange2 = 0;
                InitTimer();
            }
            if (e.KeyCode == Keys.S)
            {
                _isDown = true;
                _cordChange1 = -0.004;
                _cordChange2 = 0;
                InitTimer();
            }
            if (e.KeyCode == Keys.D)
            {
                _isDown = true;
                _cordChange1 = 0;
                _cordChange2 = 0.004;
                InitTimer();
            }
            if (e.KeyCode == Keys.A)
            {
                _isDown = true;
                _cordChange1 = 0;
                _cordChange2 = -0.004;
                InitTimer();
            }

        }
        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 600; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!_isDown)
            {
                return;
            }
            lat += _cordChange1;
            lon += _cordChange2;
            button1.PerformClick();
        }

        private void Form3_KeyUp(object sender, KeyEventArgs e)
        {
            if (ModifierKeys.HasFlag(Keys.Control))
            {
                _ctrl = false;
                return;
            }
            if (_isDown)
            {
                timer1.Stop();
                _isDown = false;
            }

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {

                if (listBox1.SelectedIndex == 0)
                {
                    return;
                }

                var baras = _barai.ElementAt(listBox1.SelectedIndex);

                string path = "https://maps.googleapis.com/maps/api/distancematrix/json?units=imperial&origins=" + _location + "&destinations=" + baras.Coordinates + "&key=AIzaSyCttVX1wln7i0nbsgnIcr9vfmYUO94oS8g";

                string matVienetai = "unknown";
                using (var reader = new StreamReader(GetStreamFromUrl(path)))
                {
                    int i = 0;
                    string st;
                    st = reader.ReadLine();
                    while ((st = reader.ReadLine()) != null)
                    {
                        while (!(st.Contains("distance")))
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
                        String distance = reader.ReadLine();
                        if (distance.Contains("mi"))
                        {
                            distance = Regex.Replace(distance, "[^0-9.]", "");
                            double temp = Math.Round(double.Parse(distance, CultureInfo.InvariantCulture) * 1.609344, 2);
                            distance = temp.ToString();
                            matVienetai = "km";
                        }
                        else if (distance.Contains("ft")){
                            distance = Regex.Replace(distance, "[^0-9.]", "");
                            double temp = Math.Round(double.Parse(distance, CultureInfo.InvariantCulture) * 0.3048, 2);
                            distance = temp.ToString();
                            matVienetai = "metrai";
                        }
                        while (!(st.Contains("duration")))
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
                        String duration = reader.ReadLine();
                        duration = Regex.Replace(duration, "[^0-9.]", "");
                        MessageBox.Show("Atstumas: " + distance + matVienetai + Environment.NewLine + "Uztruks: " + duration + "min");
                    }

                }
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                listBox1_DoubleClick(null, null);
            }

            e.SuppressKeyPress = true;
        }
    }
}
