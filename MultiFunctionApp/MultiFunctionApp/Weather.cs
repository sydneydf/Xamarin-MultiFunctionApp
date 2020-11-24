using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MultiFunctionApp
{
    /// <summary>
    /// Cast nested object to type Weather
    /// </summary>
    internal class Weather
    {
        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("main")]
        public string main { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("icon")]
        public string icon { get; set; }
    }
}