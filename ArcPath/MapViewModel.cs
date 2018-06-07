using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;
using ArcPath.Logic.AirTraffic;
using Windows.UI;
using ArcPath.Models;
using ArcPath.Logic;
using System.Threading;

namespace ArcPath
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        private AirTrafficManager _airTrafficManager;
        private Timer _updateTimer;
        private List<AircraftView> _aircrafts;

        private GraphicsOverlayCollection _overlays = new GraphicsOverlayCollection();
        public GraphicsOverlayCollection Overlays
        {
            get { return _overlays; }
            set { _overlays = value; OnPropertyChanged(); }
        }


        //private Map _map = new Map(Basemap.CreateImagery());
        private Map _map = new Map(BasemapType.ImageryWithLabels, 39.5501, 05.7821, 5);
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        private double _percentDisplayed;
        public double PercentDisplayed
        {
            get { return _percentDisplayed; }
            set
            {
                _percentDisplayed = value;
                _resetDisplay();
                OnPropertyChanged();
            }
        }

        public MapViewModel()
        {
            _airTrafficManager = AirTrafficManager.Instance;
            _updateTimer = new Timer((timer) => _updateData(), null, 0, 30000);
        }

        private List<AircraftView> _getAircraft()
        {
            var aircrafts = _airTrafficManager.GetAllAirborneAircraft();

            var aircraftViews = new List<AircraftView>();
            aircrafts.ForEach(aircraft => aircraftViews.Add(AircraftView.MapFromObject(aircraft)));

            return aircraftViews;
        }

        private void _updateData()
        {

            _aircrafts = _getAircraft();
            _resetDisplay();
        }

        private List<Graphic> _addAircraftGraphics(List<Graphic> graphics, List<AircraftView> aircrafts)
        {
            foreach(var aircraft in aircrafts)
            {
                if (aircraft.Latitude.HasValue && aircraft.Longitude.HasValue)
                {
                    var location = new MapPoint(aircraft.Longitude.Value, aircraft.Latitude.Value, SpatialReferences.Wgs84);

                    var graphic = new Graphic(location, aircraft.MapSymbol);
                    graphics.Add(graphic);
                }
            }
            return graphics;
        }

        private void _resetDisplay()
        {
            var percentage = (PercentDisplayed / 100);

            if (_aircrafts != null)
            {
                var aircraftGraphics = new List<Graphic>();
                var displayAircraft = _aircrafts.Take((int)(_aircrafts.Count * percentage)).ToList();
                aircraftGraphics = _addAircraftGraphics(aircraftGraphics, displayAircraft);

                var aircaftOverlay = new GraphicsOverlay();
                aircraftGraphics.ForEach(graphic => aircaftOverlay.Graphics.Add(graphic));

                _overlays.Clear();
                _overlays.Add(aircaftOverlay);
            }

        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
