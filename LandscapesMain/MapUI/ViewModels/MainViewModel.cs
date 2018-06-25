using MapUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;

namespace MapUI.ViewModels
{
    public class MainViewModel
    {
        private MapControl mapControl;
        public void Initialize(MapControl mapControl)
        {
            this.mapControl = mapControl;
        }
        public async Task ShowStreetside(LocationHelper location)
        {
            BasicGeoposition position = new BasicGeoposition()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            Geopoint point = new Geopoint(position);
            mapControl.Style = MapStyle.AerialWithRoads;
            MapScene hwScene = MapScene.CreateFromLocationAndRadius(point, 80, 0, 60);
            await mapControl.TrySetSceneAsync(hwScene, MapAnimationKind.Bow);
        }
        public void ShowIcon(LocationHelper location)
        {
            var myLandmarks = new List<MapElement>();
            BasicGeoposition position = new BasicGeoposition()
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude
            };
            Geopoint point = new Geopoint(position);
            var icon = new MapIcon
            {
                Location = point,
                NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0),
                ZIndex = 0,
                Title = "Searched location"
            };
            myLandmarks.Add(icon);
            var LandmarksLayer = new MapElementsLayer {
                ZIndex = 1,
                MapElements = myLandmarks
            };
            mapControl.Layers.Add(LandmarksLayer);
            mapControl.Center = point;
        }
    }
}
