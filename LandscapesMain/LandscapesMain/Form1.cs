using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Devices.Geolocation;

namespace LandscapesMain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtLat.Text.Length == 0)
            {
                statusStrip1.Items[1].ForeColor = Color.Red;
                statusStrip1.Items[1].Text = "Latitude is not entered";
                return;
            }
            if(txtLon.Text.Length == 0)
            {
                statusStrip1.Items[1].ForeColor = Color.Red;
                statusStrip1.Items[1].Text = "Longitude is not entered";
                return;
            }
            double lat = 0;
            double lon = 0;
            try
            {
                if (txtLat.Text.Substring(0, 1) == "-")
                {
                    lat = -(double.Parse(txtLat.Text.Substring(1, txtLon.Text.Length - 1), NumberStyles.AllowDecimalPoint));
                }
                else
                {
                    lat = double.Parse(txtLat.Text, NumberStyles.AllowDecimalPoint);
                }
            }
            catch(FormatException)
            {
                statusStrip1.Items[1].ForeColor = Color.Red;
                statusStrip1.Items[1].Text = "Latitude is not valid";
                return;
            }
            try
            {
                if(txtLon.Text.Substring(0, 1) == "-")
                {
                    lon = -(double.Parse(txtLon.Text.Substring(1, txtLon.Text.Length -1), NumberStyles.AllowDecimalPoint));
                }
                else
                {
                    lon = double.Parse(txtLon.Text, NumberStyles.AllowDecimalPoint);
                }
            }
            catch(FormatException)
            {
                statusStrip1.Items[1].ForeColor = Color.Red;
                statusStrip1.Items[1].Text = "Longitude is not valid";
                return;
            }
            string str = "mapuidemo://";
            Uri uri = new Uri(str + "location?lat=" + lat.ToString() + "&lon=" + lon.ToString());
            var isSuccess = Windows.System.Launcher.LaunchUriAsync(uri).AsTask().Result;
            if(isSuccess)
            {
                statusStrip1.Items[1].ForeColor = Color.Green;
                statusStrip1.Items[1].Text = "Successfully launch MapUI.exe";
            }
            else
            {
                statusStrip1.Items[1].ForeColor = Color.Red;
                statusStrip1.Items[1].Text = "Cannot launch MapUI.exe";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var locator = new Geolocator();
            var location = locator.GetGeopositionAsync().AsTask().Result;
            var position = location.Coordinate.Point.Position;
            txtLat.Text = position.Latitude.ToString();
            txtLon.Text = position.Longitude.ToString();
        }
    }
}
