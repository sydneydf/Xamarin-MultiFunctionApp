using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MultiFunctionApp
{
    //Cast nested object to type Temp
    public class Temp
    {
        [JsonProperty("day")]
        public double day { get; set; }

        [JsonProperty("min")]
        public double min { get; set; }

        [JsonProperty("max")]
        public double max { get; set; }

        [JsonProperty("night")]
        public double night { get; set; }

        [JsonProperty("eve")]
        public double eve { get; set; }

        [JsonProperty("morn")]
        public double morn { get; set; }
    }
}