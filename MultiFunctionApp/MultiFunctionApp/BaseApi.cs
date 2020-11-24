using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MultiFunctionApp
{
    //The root of our API json return, We attempt to use our Deserializer to cast into this type of object with cascading effects for nested elements
    internal class BaseApi
    {
        [JsonProperty("lat")]
        public double lat { get; set; }

        [JsonProperty("lon")]
        public double lon { get; set; }

        [JsonProperty("timezone")]
        public string timezone { get; set; }

        [JsonProperty("timezone_offset")]
        public int timezone_offset { get; set; }


        //Cast the big daily JSON object to a list of 8? DayHandler objects
        [JsonProperty("daily")]
        public IList<DayHandler> daily { get; set; }
    }
}