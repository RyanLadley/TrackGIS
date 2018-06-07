using ArcPath.Logic.AirTraffic;
using ArcPath.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcPath.Logic
{
    public class AirTrafficManager
    {
        private static AirTrafficManager _instance;
        public static AirTrafficManager Instance {
            get
            {
                if (_instance == null)
                    _instance = new AirTrafficManager();
                return _instance;
            }
        }
        private OpenSkyHandle _openSkyHandle;
        
        private AirTrafficManager()
        {
            _openSkyHandle = new OpenSkyHandle();
        }

        public List<Aircraft> GetAllAircraft()
        {
            var airTrafic = _openSkyHandle.GetAircrafts().Result;
            return airTrafic;
        }

        public List<Aircraft> GetAllAirborneAircraft()
        {
            var aricrafts = GetAllAircraft();
            return aricrafts.Where(aircraft => !aircraft.OnGround).ToList();
        }
    }
}
