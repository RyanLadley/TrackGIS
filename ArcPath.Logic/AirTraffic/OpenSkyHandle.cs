using ArcPath.Logic.Models;
using ArcPath.Logic.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace ArcPath.Logic.AirTraffic
{
    /// <summary>
    /// Handle For the OpenSky API
    /// Documentation: https://opensky-network.org/apidoc/rest.html
    /// </summary>
    public class OpenSkyHandle
    {
        private ExternalApi _openSkyApi;

        public OpenSkyHandle()
        {
            _openSkyApi = new ExternalApi("https://opensky-network.org/api");
        }


        /// <summary>
        /// Get All Aircraft
        /// Wrapper for GET /states/all
        /// </summary>
        /// <returns></returns>
        public async Task<List<Aircraft>> GetAircrafts()
        {
            try
            {
                using (var response = _openSkyApi.Get("states/all").Result)
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonRepsonse = JsonConvert.DeserializeObject<JObject>(response.Content.ReadAsStringAsync().Result);
                        var states = jsonRepsonse.GetValue("states").ToObject<JArray>();

                        var aircrafts = new List<Aircraft>();
                        foreach (JArray state in states)
                            aircrafts.Add(Aircraft.MapFromOpenSky(state));

                        return aircrafts;
                    }
                }
                //Failed State
                return null;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
