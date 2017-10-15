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
using Alus.GoogleApi;
using Newtonsoft.Json;

namespace Alus
{

    public partial class LocationForm : Form
    {
        private bool _isDown = false;
        private double _cordChange1 = 0;
        private double _cordChange2 = 0;
        public bool _ieskoti = true;
        private List<Bar> _barList;
        private int _zoom = 12;
        private bool _ctrl = false;
        private NearestBars nearestBars = new NearestBars();
        IEnumerable<Location> directions = null;
        public double lat;
        public double lon;

        public LocationForm()
        {
            InitializeComponent();
            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_ieskoti == true)
            {
                _barList = nearestBars.Location();
                lat = nearestBars._location.Latitude;
                lon = nearestBars._location.Longtitude;
                listBox1.Items.Add("* - Your location");
            }

            
            string path;
            string centerLocation = new Location(lat, lon).ToString();
            string currentLocation = nearestBars._location.ToString();
            path = "https://maps.googleapis.com/maps/api/staticmap?center=" + centerLocation + "&zoom=" + _zoom.ToString() + "&size=400x400&markers=color:blue%7Clabel:*%7C" + currentLocation;
            int count = 'A';
                if (_barList != null)
                {
                    foreach (Bar baras in _barList)
                    {
                        path = path + "&markers=color:blue%7Clabel:" + (char)count + "%7C" + baras.Coordinates;
                    if (_ieskoti == true)
                    {
                        listBox1.Items.Add((char)count + " - " + baras.Name);
                    }
                    count++;
                }
            }
            _ieskoti = false;
            path = path + "&key=AIzaSyARqcyQXKX0gz1NG4ulXlDdnqDCNS_bJrU"; // API key

            pictureBox1.Image = Image.FromStream(nearestBars.GetStreamFromUrl(path));
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

        private Alus.GoogleApi.Element GetDistanceElement(Location origin, Bar destinationBar)
        {
            string url = "https://maps.googleapis.com/maps/api/distancematrix/json?units=metric&origins=" + origin + "&destinations=" + destinationBar.Coordinates + "&key=AIzaSyCttVX1wln7i0nbsgnIcr9vfmYUO94oS8g";

            using (var reader = new JsonTextReader(new StreamReader(nearestBars.GetStreamFromUrl(url))))
            {
                var serializer = new JsonSerializer();
                var response = serializer.Deserialize<DistanceMatrixRequest>(reader);
                if (response.Status == "OK")
                {
                    return response.Rows[0].Elements[0];
                }
                else
                {
                    return null;
                }
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

                var bar = _barList.ElementAt(listBox1.SelectedIndex - 1);
                var element = GetDistanceElement(nearestBars._location, bar);
                MessageBox.Show("Distance: " + element.Distance.Text + Environment.NewLine + "Duration: " + element.Duration.Text);
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
