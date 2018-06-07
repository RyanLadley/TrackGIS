using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Geometry;
using ArcPath.Logic.AirTraffic;
using Windows.UI;
using System.Threading;

namespace ArcPath
{
	/// <summary>
	/// A map page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{

		public MainPage()
		{
			this.InitializeComponent();
            
            MapView.Map = MapModel.Map;
            MapView.GraphicsOverlays = MapModel.Overlays;

            //var openSkyHandle = new OpenSkyHandle();
            //var aircrafts = openSkyHandle.GetAircrafts().Result;

        }

        /// <summary>
        /// Gets the view-model that provides mapping capabilities to the view
        /// </summary>
        public MapViewModel MapModel { get; } = new MapViewModel();

        private void CountSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            var slider = (Slider)sender;
            MapModel.PercentDisplayed = slider.Value;
        }

        // Map initialization logic is contained in MapViewModel.cs


    }
}
