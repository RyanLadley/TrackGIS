using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace ArcPath.Logic.Models
{
    public class Aircraft
    {
        public string CountryOfOrigin { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? Altitude { get; set; }
        public bool OnGround { get; set; }
        public double? Velocity { get; set; }
        public double? Heading { get; set; }
        public double? VerticalRate { get; set; }
        public string Squawk { get; set; }
        
        public static Aircraft MapFromOpenSky(JArray apiResponse)
        {
            return new Aircraft()
            {
                CountryOfOrigin = (string)apiResponse[2],
                Longitude = (double?)apiResponse[5],
                Latitude = (double?)apiResponse[6],
                Altitude = (double?)apiResponse[7],
                OnGround = (bool)apiResponse[8],
                Velocity = (double?)apiResponse[9],
                Heading = (double?)apiResponse[10],
                VerticalRate = (double?)apiResponse[11],
                Squawk = (string)apiResponse[14]
            };
        }
    }
}
