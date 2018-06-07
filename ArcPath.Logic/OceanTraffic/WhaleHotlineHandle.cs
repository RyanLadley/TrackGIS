using ArcPath.Logic.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArcPath.Logic.OceanTraffic
{
    public class WhaleHotlineHandle
    {
        private ExternalApi _whaleHotlineApi;

        public WhaleHotlineHandle()
        {
            _whaleHotlineApi = new ExternalApi("http://hotline.whalemuseum.org");
        }
        
    }
    
    public struct SpeciesParameter
    {
        public const string PACIFIC_DOLPHIN = "pacific white-sided dolphin";
        public const string ATLANTIC_DOLPHIN = "atlantic  white-sided dolphin";
        public const string OPRCA = "orca";
        public const string HUMPBACK_WHALE = "humpback";
        public const string GRAY_WHALE = "gray whale";
    }


}
