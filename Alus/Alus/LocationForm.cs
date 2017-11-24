﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Alus.Core.Models;

namespace Alus
{
    public partial class LocationForm : ChildForm
    {
        private readonly IColorPicker _colorPicker;
        private readonly double _diretionWeight = 0.004;

        private bool _isDown = false;
        private bool _firstRun = true;
        private List<Bar> _barList;
        private int _zoom = 12;
        private bool _ctrl = false;
        Location _centerLocation;
        Vector2d _direction;
        private NearestBars nearestBars = new NearestBars();
        IEnumerable<Location> directions = null;

        public LocationForm(IColorPicker colorPicker)
        {
            InitializeComponent();
            _colorPicker = colorPicker;
            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;
        }

        private char LetterAt(int index, char start = 'A')
        {
            return (char)((int)start + index);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_firstRun == true)
            {
                _barList = nearestBars.FindBars();
                _centerLocation = nearestBars.Location;
                listBox1.Items.Add("* - Your location");
                int i = 0;
                foreach (var bar in _barList)
                {
                    listBox1.Items.Add($"{LetterAt(i)} - {bar.Name}");
                    i++;
                }
                _firstRun = false;
            }

            var mapRequest = new MapRequest()
            {
                Size = pictureBox1.Size,
                Center = _centerLocation,
                Zoom = _zoom
            };

            var labels = _barList
                .Select((bar, index) => new Label() { Color = _colorPicker.GetColor(bar.PlaceId), Name = LetterAt(index).ToString() , Location = new Location(bar.Coordinates) })
                .Concat(new[] { new Label() { Color = Color.Red, Location = nearestBars.Location, Name = "*" } });

            using (var stream = nearestBars.GetMap(mapRequest, labels, directions))
            {
                pictureBox1.Image = Image.FromStream(stream);
            }
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
            Close();
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
                _direction = Vector2d.UnitX;
                InitTimer();
            }
            if (e.KeyCode == Keys.S)
            {
                _isDown = true;
                _direction = -Vector2d.UnitX;
                InitTimer();
            }
            if (e.KeyCode == Keys.D)
            {
                _isDown = true;

                _direction = Vector2d.UnitY;
                InitTimer();
            }
            if (e.KeyCode == Keys.A)
            {
                _isDown = true;
                _direction = -Vector2d.UnitY;
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
            _centerLocation += (_diretionWeight * _direction);
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
                    directions = null;
                    button1.PerformClick();
                    return;
                }
                var bar = _barList.ElementAt(listBox1.SelectedIndex - 1);

                var origin = nearestBars.Location;
                var destination = new Alus.Location(bar.Coordinates);
                var element = nearestBars.GetDistanceElement(origin, destination);
                directions = nearestBars.GetDirections(origin, destination);

                MessageBox.Show("Distance: " + element.Distance.Text + Environment.NewLine + "Duration: " + element.Duration.Text);
                button1.PerformClick();
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
