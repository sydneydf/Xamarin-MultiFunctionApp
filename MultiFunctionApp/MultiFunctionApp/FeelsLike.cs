using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MultiFunctionApp
{
    //Feels_like nested object
    internal class FeelsLike
    {
        [JsonProperty("day")]
        public double day { get; set; }

        [JsonProperty("night")]
        public double night { get; set; }

        [JsonProperty("eve")]
        public double eve { get; set; }

        [JsonProperty("morn")]
        public double morn { get; set; }
    }
}