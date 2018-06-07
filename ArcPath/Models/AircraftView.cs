using ArcPath.Logic.Models;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcPath.Models
{
    public class AircraftView
    {
        public PictureMarkerSymbol MapSymbol { get; set; }

        public string CountryOfOrigin { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Altitude { get; set; }
        public double? Velocity { get; set; }
        public double? Heading { get; set; }
        public double? VerticalRate { get; set; }
        public string Squawk { get; set; }

        public static AircraftView MapFromObject(Aircraft obj)
        {// define a picture fill symbol using a png on disk
            var mapSymbol = new PictureMarkerSymbol(new Uri($"{Directory.GetCurrentDirectory()}/Icons/aircraft.png"));
            mapSymbol.Angle = obj.Heading ?? 0;

            return new AircraftView()
            {
                CountryOfOrigin = obj.CountryOfOrigin,
                Longitude = obj.Longitude,
                Latitude = obj.Latitude,
                Velocity = obj.Velocity,
                Heading = obj.Heading,
                VerticalRate = obj.VerticalRate,
                Squawk = obj.Squawk,
                MapSymbol = mapSymbol
            };
        }
    }
}